using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDLL.Core.ErrorController.Domain
{
    public partial class ErrorReport
    {
        #region Main Prop
        public long id { get; set; }

        public DateTime? date { get; set; }

        public string Message1 { get; set; }

        public string Message2 { get; set; }

        public string Message3 { get; set; }

        public string Message4 { get; set; }

        public string Message5 { get; set; }

        public string StackTrace1 { get; set; }

        public string StackTrace2 { get; set; }

        public string StackTrace3 { get; set; }

        public string StackTrace4 { get; set; }

        public string StackTrace5 { get; set; }

        public string layer1 { get; set; }

        public string layer2 { get; set; }

        public string layer3 { get; set; }

        public string layer4 { get; set; }

        public string layer5 { get; set; }

        public string layer6 { get; set; }

        public string layer7 { get; set; }

        public string layer8 { get; set; }

        public string layer9 { get; set; }

        public string layer10 { get; set; }

        public string layer11 { get; set; }

        public string layer12 { get; set; }

        public string layer13 { get; set; }

        public string layer14 { get; set; }

        public string layer15 { get; set; }

        public string layer16 { get; set; }

        public string layer17 { get; set; }

        public string layer18 { get; set; }

        public string layer19 { get; set; }

        public string layer20 { get; set; }

        public string Program { get; set; }
        #endregion

        #region Combine Properties
        [JsonIgnore]
        [NotMapped]
        public string Message
        {
            get
            { return (Message1 ?? "") + (Message2 ?? "") + (Message3 ?? "") + (Message4 ?? "") + (Message5 ?? ""); }
            set
            {
                Message1 = Message2 = Message3 = Message4 = Message5 = null;
                if (string.IsNullOrWhiteSpace(value))
                    return;

                int len = 0;

                len = Math.Min(value.Length, 3990);
                Message1 = value.Substring(0, len);
                value = value.Replace(Message1, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                Message2 = value.Substring(0, len);
                value = value.Replace(Message2, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                Message3 = value.Substring(0, len);
                value = value.Replace(Message3, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                Message4 = value.Substring(0, len);
                value = value.Replace(Message4, "");
                if (string.IsNullOrEmpty(value)) return;

                Message5 = value;
            }
        }

        [JsonIgnore]
        [NotMapped]
        public string StackTrace
        {
            get
            { return (StackTrace1 ?? "") + (StackTrace2 ?? "") + (StackTrace3 ?? "") + (StackTrace4 ?? "") + (StackTrace5 ?? ""); }
            set
            {
                StackTrace1 = StackTrace2 = StackTrace3 = StackTrace4 = StackTrace5 = null;
                if (string.IsNullOrWhiteSpace(value))
                    return;

                int len = 0;

                len = Math.Min(value.Length, 3990);
                StackTrace1 = value.Substring(0, len);
                value = value.Replace(StackTrace1, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                StackTrace2 = value.Substring(0, len);
                value = value.Replace(StackTrace2, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                StackTrace3 = value.Substring(0, len);
                value = value.Replace(StackTrace3, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                StackTrace4 = value.Substring(0, len);
                value = value.Replace(StackTrace4, "");
                if (string.IsNullOrEmpty(value)) return;

                StackTrace5 = value;
            }
        }

        [JsonIgnore]
        [NotMapped]
        public string layer
        {
            get
            { return (layer1 ?? "") + (layer2 ?? "") + (layer3 ?? "") + (layer4 ?? "") + (layer5 ?? "") + (layer6 ?? "") + (layer7 ?? "") + (layer8 ?? "") + (layer9 ?? "") + (layer10 ?? "") + (layer11 ?? "") + (layer12 ?? "") + (layer13 ?? "") + (layer14 ?? "") + (layer15 ?? "") + (layer16 ?? "") + (layer17 ?? "") + (layer18 ?? "") + (layer19 ?? "") + (layer20 ?? ""); }
            set
            {
                layer1 = layer2 = layer3 = layer4 = layer5 = layer6 = layer7 = layer8 = layer9 = layer10 = layer11 = layer12 = layer13 = layer14 = layer15 = layer16 = layer17 = layer18 = layer19 = layer20 = null;
                if (string.IsNullOrWhiteSpace(value))
                    return;

                int len = 0;

                len = Math.Min(value.Length, 3990);
                layer1 = value.Substring(0, len);
                value = value.Replace(layer1, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer2 = value.Substring(0, len);
                value = value.Replace(layer2, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer3 = value.Substring(0, len);
                value = value.Replace(layer3, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer4 = value.Substring(0, len);
                value = value.Replace(layer4, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer5 = value.Substring(0, len);
                value = value.Replace(layer5, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer6 = value.Substring(0, len);
                value = value.Replace(layer6, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer7 = value.Substring(0, len);
                value = value.Replace(layer7, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer8 = value.Substring(0, len);
                value = value.Replace(layer8, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer9 = value.Substring(0, len);
                value = value.Replace(layer9, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer10 = value.Substring(0, len);
                value = value.Replace(layer10, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer11 = value.Substring(0, len);
                value = value.Replace(layer11, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer12 = value.Substring(0, len);
                value = value.Replace(layer12, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer13 = value.Substring(0, len);
                value = value.Replace(layer13, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer14 = value.Substring(0, len);
                value = value.Replace(layer14, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer15 = value.Substring(0, len);
                value = value.Replace(layer15, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer16 = value.Substring(0, len);
                value = value.Replace(layer16, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer17 = value.Substring(0, len);
                value = value.Replace(layer17, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer18 = value.Substring(0, len);
                value = value.Replace(layer18, "");
                if (string.IsNullOrEmpty(value)) return;

                len = Math.Min(value.Length, 3990);
                layer19 = value.Substring(0, len);
                value = value.Replace(layer19, "");
                if (string.IsNullOrEmpty(value)) return;

                layer20 = value;
            }
        }
        #endregion

        #region New Instance
        public static ErrorReport New_Instance(ErrorReport item)
        {
            ErrorReport dm = new ErrorReport()
            {
                id = item.id,
                date = item.date,
                Message1 = item.Message1,
                Message2 = item.Message2,
                Message3 = item.Message3,
                Message4 = item.Message4,
                Message5 = item.Message5,
                StackTrace1 = item.StackTrace1,
                StackTrace2 = item.StackTrace2,
                StackTrace3 = item.StackTrace3,
                StackTrace4 = item.StackTrace4,
                StackTrace5 = item.StackTrace5,
                layer1 = item.layer1,
                layer2 = item.layer2,
                layer3 = item.layer3,
                layer4 = item.layer4,
                layer5 = item.layer5,
                layer6 = item.layer6,
                layer7 = item.layer7,
                layer8 = item.layer8,
                layer9 = item.layer9,
                layer10 = item.layer10,
                layer11 = item.layer11,
                layer12 = item.layer12,
                layer13 = item.layer13,
                layer14 = item.layer14,
                layer15 = item.layer15,
                layer16 = item.layer16,
                layer17 = item.layer17,
                layer18 = item.layer18,
                layer19 = item.layer19,
                layer20 = item.layer20,
                Program = item.Program
            };
            return dm;
        }
        public ErrorReport NewInstance()
        { return New_Instance(this); }
        #endregion

        public override bool Equals(object obj)
        {
            try
            {
                if (obj is null || !(obj is ErrorReport))
                    return false;

                var data2 = obj as ErrorReport;

                return
                    this.id == data2.id &&
                    this.Message1 == data2.Message1 &&
                    this.Message2 == data2.Message2 &&
                    this.Message3 == data2.Message3 &&
                    this.Message4 == data2.Message4 &&
                    this.Message5 == data2.Message5 &&
                    this.StackTrace1 == data2.StackTrace1 &&
                    this.StackTrace2 == data2.StackTrace2 &&
                    this.StackTrace3 == data2.StackTrace3 &&
                    this.StackTrace4 == data2.StackTrace4 &&
                    this.StackTrace5 == data2.StackTrace5 &&
                    this.layer1 == data2.layer1 &&
                    this.layer2 == data2.layer2 &&
                    this.layer3 == data2.layer3 &&
                    this.layer4 == data2.layer4 &&
                    this.layer5 == data2.layer5 &&
                    this.layer6 == data2.layer6 &&
                    this.layer7 == data2.layer7 &&
                    this.layer8 == data2.layer8 &&
                    this.layer9 == data2.layer9 &&
                    this.layer10 == data2.layer10 &&
                    this.layer11 == data2.layer11 &&
                    this.layer12 == data2.layer12 &&
                    this.layer13 == data2.layer13 &&
                    this.layer14 == data2.layer14 &&
                    this.layer15 == data2.layer15 &&
                    this.layer16 == data2.layer16 &&
                    this.layer17 == data2.layer17 &&
                    this.layer18 == data2.layer18 &&
                    this.layer19 == data2.layer19 &&
                    this.layer20 == data2.layer20 &&
                    this.Program == data2.Program;
            }
            catch { return false; }
        }
        public override int GetHashCode()
        { return base.GetHashCode(); }

        #region Operator
        public static bool operator ==(ErrorReport data1, ErrorReport data2)
        {
            if (data1 is null ^ data2 is null)
                return false;
            if (data1 is null && data2 is null)
                return true;

            return data1.Equals(data2);
        }
        public static bool operator !=(ErrorReport data1, ErrorReport data2)
        {
            if (data1 == data2)
                return false;
            return true;
        }
        #endregion
    }
}