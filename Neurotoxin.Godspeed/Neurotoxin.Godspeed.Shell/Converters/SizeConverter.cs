using System;
using Neurotoxin.Godspeed.Presentation.Bindings;
using Neurotoxin.Godspeed.Shell.Constants;
using Neurotoxin.Godspeed.Shell.ViewModels;
using Resx = Neurotoxin.Godspeed.Shell.Properties.Resources;

namespace Neurotoxin.Godspeed.Shell.Converters
{
    public class SizeConverter : MultiBindingConverterBase<FileSystemItemViewModel, string, SizeConverterParameter>
    {
        public string Suffix { get; set; }

        private string[] SuffixName = { "B", "KB", "MB", "GB", "TB", "PB" };

        public SizeConverter() : base(new [] { "Size", "IsRefreshing", "Type", "TitleType" })
        {
        }

        protected override string Convert(FileSystemItemViewModel viewModel, Type targetType, SizeConverterParameter parameter)
        {
            if (viewModel == null) return null;

            switch (parameter)
            {
                case SizeConverterParameter.Plain:
                    return SizeFormat(viewModel.Size);
                case SizeConverterParameter.ListView:
                case SizeConverterParameter.ContentView:
                    if (viewModel.IsRefreshing) return "?";

                    //size is set
                    if (viewModel.Size != null) return SizeFormat(viewModel.Size);

                    string resxName;
                    if (viewModel.TitleType != TitleType.Unknown)
                    {
                        resxName = viewModel.TitleType.GetType().Name + viewModel.TitleType;
                        if (parameter == SizeConverterParameter.ContentView) resxName += "Long";
                    }
                    else
                        resxName = viewModel.Type.GetType().Name + viewModel.Type;

                    var rm = Resx.ResourceManager;
                    return parameter == SizeConverterParameter.ListView
                               ? string.Format("<{0}>", rm.GetString(resxName).ToUpper())
                               : rm.GetString(resxName);
                default:
                    throw new NotSupportedException("Invalid parameter value: " + parameter);
            }
        }

        private string SizeFormat(long? size)
        {
            if (size.HasValue)
            {
                int c;
                for (c = 0; c < SuffixName.Length; c++)
                {
                    long m = 1 << ((c + 1) * 10);
                    if (size.Value < m)
                        break;
                }
                double n = size.Value / (double)(1 << (c * 10));
                return String.Format("{0:0.##} {1}", n, SuffixName[c]).Trim();
            }
            return null;
        }

    }
}