using MediatR;

namespace IncomeFollowUp.Application.Settings.Queries.GetSettings;

public record GetSettingsQuery : IRequest<Domain.Settings?>;
