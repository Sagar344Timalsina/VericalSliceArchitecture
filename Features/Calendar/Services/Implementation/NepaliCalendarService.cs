using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using verticalSliceArchitecture.Data;
using verticalSliceArchitecture.Domain;
using verticalSliceArchitecture.Features.Calendar.DTOs;
using verticalSliceArchitecture.Features.Calendar.Services.Interface;
using verticalSliceArchitecture.Shared;
using MediatR;

namespace verticalSliceArchitecture.Features.Calendar.Services.Implementation
{
    public class NepaliCalendarService : INepaliCalendarService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public NepaliCalendarService(ApplicationDbContext dbContext, IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<PaginatedResponse<NepaliCalendarDto>> GetCalendarAsync(CalendarQueryParametersDTO parameters)
        {
            var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("No HttpContext available.");
            var urlHelper = _urlHelperFactory.GetUrlHelper(new ActionContext(httpContext, httpContext.GetRouteData(), new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()));
            var query = _dbContext.NepaliCalendar
             .Include(nc => nc.NepaliMonth) // Ensure Include() is first
            .Where(nc => nc.Year == parameters.Year && nc.MonthId == parameters.Month);

            if (parameters.ShowHolidaysOnly)
                query = query.Where(nc => nc.IsHoliday);

            query = parameters.SortBy.ToLower() switch
            {
                "day" => parameters.Descending ? query.OrderByDescending(nc => nc.Day) : query.OrderBy(nc => nc.Day),
                "englishDate" => parameters.Descending ? query.OrderByDescending(nc => nc.EnglishDate) : query.OrderBy(nc => nc.EnglishDate),
                _ => query.OrderBy(nc => nc.Day)
            };
            int totalRecords = await query.CountAsync();

            var paginatedData = await query
           .Skip((parameters.PageNumber - 1) * parameters.PageSize)
           .Take(parameters.PageSize)
           .ToListAsync();

            var results = paginatedData
              .Select(nc => new NepaliCalendarDto
              {
                  Id = nc.Id,
                  Year = nc.Year,
                  Month = nc.MonthId,
                  Day = nc.Day,
                  EnglishDate = nc.EnglishDate,
                  WeekDay = nc.WeekDay,
                  IsHoliday = nc.IsHoliday,
                  HolidayName = nc.HolidayName,
                  NepaliMonthName = nc.NepaliMonth.MonthName // Avoids including full NepaliMonth
              }).ToList();

            var totalPages = (int)Math.Ceiling((double)totalRecords / parameters.PageSize);

            var links = new Dictionary<string, string>
            {
                // "self" link, pointing to the current page
                { "self", urlHelper.Link("GetNepaliCalendar", new { Year = parameters.Year, Month = parameters.Month, PageNumber = parameters.PageNumber, PageSize = parameters.PageSize }) }
            };


            if (parameters.PageNumber < totalPages)
            {
                links["next"] = urlHelper.Link("GetNepaliCalendar", new
                {
                    Year = parameters.Year,
                    Month = parameters.Month,
                    PageNumber = parameters.PageNumber + 1,
                    PageSize = parameters.PageSize
                });
            }

            if (parameters.PageNumber > 1)
            {
                links["prev"] = urlHelper.Link("GetNepaliCalendar", new
                {
                    Year = parameters.Year,
                    Month = parameters.Month,
                    PageNumber = parameters.PageNumber - 1,
                    PageSize = parameters.PageSize
                });
            }

 
            return new PaginatedResponse<NepaliCalendarDto>
            {
                Data = results,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Links = links 
            };
        }
    }
}
