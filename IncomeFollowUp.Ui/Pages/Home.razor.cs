using System.Reflection.Emit;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor.Util;
using IncomeFollowUp.Ui.Services;
using Microsoft.AspNetCore.Components;

namespace IncomeFollowUp.Ui.Pages;

public partial class Home
{
    [Inject]
    public WorkDaysService WorkDaysService { get; set; } = null!;
    private readonly LineConfig _config =  new()
    {
          Options = new LineOptions
          {
            Responsive = true,
            Title = new OptionsTitle
            {
                Display = false,
            },
            Scales = new Scales
            {
                YAxes =
                [
                    new LinearCartesianAxis
                    {
                        Ticks = new LinearCartesianTicks
                        {
                            BeginAtZero = true,
                        },
                    },
                ],
            },
            
          }
        };

    protected override async Task OnInitializedAsync()
    {
        var workDaysSummaryDtos = await WorkDaysService.GetSummary();
        
        foreach(var label in workDaysSummaryDtos.Select(x => $"{x.Month}/{x.Year}").AsEnumerable())
        {
            _config.Data.Labels.Add(label);
        }

        _config.Data.Datasets.Add(new LineDataset<int>(workDaysSummaryDtos.Select(x => x.Total))
        {
            Label = "Monthly Income",
            BorderColor = ColorUtil.ColorHexString(252, 127, 25),
            Fill = false,
        });
    }
}