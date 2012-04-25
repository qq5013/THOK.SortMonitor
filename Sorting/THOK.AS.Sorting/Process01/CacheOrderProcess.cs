using System;
using System.Collections.Generic;
using System.Text;
using THOK.AS.Sorting.Util;
using THOK.MCP;

namespace THOK.AS.Sorting.Process
{
    class CacheOrderProcess : AbstractProcess
    {
        private MessageUtil messageUtil = null;

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                messageUtil = new MessageUtil(context.Attributes);
            }
            catch (Exception e)
            {
                Logger.Error("CacheOrderProcess 初始化失败！原因：" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                object stateCacheA = Context.Services["SortPLC"].Read("CacheOrderSortNoesA");
                object stateCacheB = Context.Services["SortPLC"].Read("CacheOrderSortNoesB");
                object stateBarCode1 = Context.Services["SortPLC"].Read("CacheOrderSortNoesBarCode1");
                object stateBarCode2 = Context.Services["SortPLC"].Read("CacheOrderSortNoesBarCode2");
                object statePacker1 = Context.Services["SortPLC"].Read("CacheOrderSortNoesPacker1");
                object statePacker2 = Context.Services["SortPLC"].Read("CacheOrderSortNoesPacker2");

                int[] sortNoesA = new int[3];
                int[] sortNoesB = new int[3];
                int[] sortNoesBarCode1 = new int[2];
                int[] sortNoesBarCode2 = new int[2];
                int[] sortNoesPacker1 = new int[2];
                int[] sortNoesPacker2 = new int[2];

                if (stateCacheA is Array && stateCacheB is Array && stateBarCode1 is Array && stateBarCode2 is Array && statePacker1 is Array && statePacker2 is Array)
                {
                    Array arrayCacheA = (Array)stateCacheA;
                    Array arrayCacheB = (Array)stateCacheB;
                    Array arrayBarCode1 = (Array)stateBarCode1;
                    Array arrayBarCode2 = (Array)stateBarCode2;
                    Array arryPacker1 = (Array)statePacker1;
                    Array arryPacker2 = (Array)statePacker2;
                    if (arrayCacheA.Length == 3 && arrayCacheB.Length == 3 && arrayBarCode1.Length == 2 && arrayBarCode2.Length == 2 && arryPacker1.Length == 2 && arryPacker2.Length == 2)
                    {
                        arrayCacheA.CopyTo(sortNoesA, 0);
                        arrayCacheB.CopyTo(sortNoesB, 0);
                        arrayBarCode1.CopyTo(sortNoesBarCode1, 0);
                        arrayBarCode2.CopyTo(sortNoesBarCode2, 0);
                        arryPacker1.CopyTo(sortNoesPacker1, 0);
                        arryPacker2.CopyTo(sortNoesPacker2, 0);


                        Dictionary<string, int> parameter = new Dictionary<string, int>();

                        parameter.Add("SwitchOneSortNo", sortNoesBarCode1[0]);
                        parameter.Add("SwitchOneChannelGroup", sortNoesBarCode1[1]);
                        parameter.Add("SwitchTwoSortNo", sortNoesBarCode2[0]);
                        parameter.Add("SwitchTwoChannelGroup", sortNoesBarCode2[1]);

                        parameter.Add("PackerOneSortNo", sortNoesPacker1[0]);
                        parameter.Add("PackerOneChannelGroup", sortNoesPacker1[1]);
                        parameter.Add("PackerTwoSortNo", sortNoesPacker2[0]);
                        parameter.Add("PackerTwoChannelGroup", sortNoesPacker2[1]);

                        //messageUtil.SendToExport(parameter);
                    }
                }

            }
            catch (Exception e)
            {
                Logger.Error("CacheOrderProcess 初始化失败！原因：" + e.Message);
            }
        }

    }
}
