﻿using NiceHashMiner.Algorithms;
using NiceHashMiner.Switching;
using NiceHashMinerLegacy.Common;
using NiceHashMinerLegacy.Common.Configs;

namespace NiceHashMiner.Miners
{
    public class ClaymoreCryptoNightMiner : ClaymoreBaseMiner
    {
        public ClaymoreCryptoNightMiner()
            : base("ClaymoreCryptoNightMiner")
        {
            LookForStart = "xmr - total speed:";
            // DevFee = 0
        }

        public override void Start(string url, string btcAdress, string worker)
        {
            var username = GetUsername(btcAdress, worker);
            LastCommandLine =
                $" {GetDevicesCommandString()} -mport 127.0.0.1:-{ApiPort} -xpool {url} -xwal {username} -xpsw x -dbg -1 -pow7 1";
            ProcessHandle = _Start();
        }

        // benchmark stuff

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            BenchmarkTimeWait = time; // Takes longer as of v10

            // network workaround
            var url = NHSmaData.GetLocationUrl(algorithm.NiceHashID,
                Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation],
                ConectionType);
            // demo for benchmark
            var username = Globals.DemoUser;
            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            return $" {GetDevicesCommandString()} -mport -{ApiPort} -xpool {url} -xwal {username} -xpsw x -logfile {GetLogFileName()} -pow7 1";
        }
    }
}
