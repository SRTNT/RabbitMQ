using System.ComponentModel;

namespace GeneralDLL.DTO.SystemEnums
{
    public enum Bot_State
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        None = 0,
        /// <summary>
        /// Start
        /// </summary>
        [Description("Start")]
        Start = 1,
        /// <summary>
        /// Stop
        /// </summary>
        [Description("Stop")]
        Stop = 2,
        /// <summary>
        /// MustStart
        /// </summary>
        [Description("MustStart")]
        MustStart = 3,
        /// <summary>
        /// MustStop
        /// </summary>
        [Description("MustStop")]
        MustStop = 4,
    }
}
