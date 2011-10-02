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
                object state = Context.Services["SortPLC"].Read("CacheOrderSortNoes");

                int[] sortNoes = new int[17];

                if (state != null && state is Array)
                {
                    Array array = (Array)state;
                    if (array.Length == 17)
                    {
                        array.CopyTo(sortNoes, 0);

                        Dictionary<string, int> parameter = new Dictionary<string, int>();

                        parameter.Add("SwitchOneSortNo", sortNoes[8]);
                        parameter.Add("SwitchOneChannelGroup", sortNoes[9]);
                        parameter.Add("SwitchTwoSortNo", sortNoes[10]);
                        parameter.Add("SwitchTwoChannelGroup", sortNoes[11]);

                        parameter.Add("PackerOneSortNo", sortNoes[12]);
                        parameter.Add("PackerOneChannelGroup", sortNoes[13]);
                        parameter.Add("PackerTwoSortNo", sortNoes[14]);
                        parameter.Add("PackerTwoChannelGroup", sortNoes[15]);

                        messageUtil.SendToExport(parameter);
                    }
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
