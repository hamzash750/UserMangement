using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Business.Interfaces;
using UserManagement.Common.Util;
using UserManagement.Repository;
using UserManagement.ViewModel;

namespace UserManagement.Business.Services
{
    public class SearchServise : ISearch
    {
        private UserManagementContext context = null;
        private ILogger logger;
        private IConfiguration configuration;
        public SearchServise(UserManagementContext context,
            ILogger<SearchServise> logger, IConfiguration configuration)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }
        public IQueryable<UserVM> GetSearch(SearchVM query)
        {
            try
            {
                this.logger.LogDebug("In SearchAlertService.GetSearch");
                IQueryable<SearchVM> SearchResult = new SearchVM[] { }.AsQueryable();
                bool SearchQuery = !string.IsNullOrEmpty(query.Name) || !string.IsNullOrEmpty(query.Designation) || query.JoiningDate != null || query.Country != null;
                SearchResult = SearchQuery ? FilterResultSearch(query) : FilterResult(query.sort, query.Sortcolumn, query.pageNumber);

                return SearchResult.Select(user => new UserVM()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Designation = user.Designation,
                    JoiningDate = user.JoiningDate,
                    Image = user.Image,
                    Country = user.Country,
                    FullAddress = user.FullAddress
                }).AsQueryable();

            }
            catch (Exception e)
            {

                this.logger.LogDebug("In SearchServise.GetSearch Exception Message= " + e.Message);
                return new UserVM[] { }.AsQueryable();
            }
            finally
            {
                this.logger.LogDebug("Out SearchServise.GetSearch");
            }


        }
        public IQueryable<SearchVM> FilterResultSearch(SearchVM query)
        {
            
            try
            {
                this.logger.LogDebug("In SearchServise.SearchServise");
                IQueryable<SearchVM> submittedResult = new SearchVM[] { }.AsQueryable();
                if (query.JoiningStartDate != null && query.JoiningEndDate != null)
                {
                    submittedResult = from user in context.Users
                                      join uaddress in context.UserAddress
                                      on user.AddressId equals uaddress.AddressId
                                      where  (user.JoiningDate >= query.JoiningStartDate && user.JoiningDate <= query.JoiningEndDate)
                                      orderby user.JoiningDate descending
                                      select new SearchVM()
                                      {
                                          Id = user.UserId,
                                          Name = user.Name,
                                          Designation = user.Designation,
                                          JoiningDate = user.JoiningDate.ToShortDateString(),
                                          Image = user.ImagePath,
                                          Country = uaddress.Country,
                                          FullAddress = uaddress.Street + " " + uaddress.State + " " + uaddress.City + " " + uaddress.PinCode
                                      };
                }
                else
                {
                    submittedResult = from user in context.Users
                                      join uaddress in context.UserAddress
                                      on user.AddressId equals uaddress.AddressId
                                      orderby user.JoiningDate descending
                                      select new SearchVM()
                                      {
                                          Id = user.UserId,
                                          Name = user.Name,
                                          Designation = user.Designation,
                                          JoiningDate = user.JoiningDate.ToShortDateString(),
                                          Image = user.ImagePath,
                                          Country = uaddress.Country,
                                          FullAddress = uaddress.Street + " " + uaddress.State + " " + uaddress.City + " " + uaddress.PinCode
                                      };
                }
                if (!string.IsNullOrEmpty(query.Name))
                {
                    string Name = query.Name.Trim();
                    submittedResult = submittedResult.Where(x => x.Name.Contains(Name));
                }
                if (!string.IsNullOrEmpty(query.Designation))
                {
                    string Designation = query.Designation.Trim();
                    submittedResult = submittedResult.Where(x => x.Designation.Contains(Designation));
                }
                if (!string.IsNullOrEmpty(query.Country))
                {
                    string Country = query.Country.Trim();
                    submittedResult = submittedResult.Where(x => x.Country.Contains(Country));
                }
                var totalRecord = submittedResult.Count();
                submittedResult = Sorting(query.sort, query.Sortcolumn, submittedResult);
                    submittedResult = PaginatedList<SearchVM>.CreateAsync(submittedResult.AsNoTracking(), query.pageNumber, 10).ToList().AsQueryable();
                if (submittedResult.Count() > 0)
                {
                    submittedResult.FirstOrDefault().TotalRecord = totalRecord.ToString();
                }
                return submittedResult;
            }
            catch (Exception e)
            {

                this.logger.LogDebug("In SearchAlertService.GetSubmittedAlertSearch Exception Message= " + e.Message);
                return new SearchVM[] { }.AsQueryable();
            }
            finally
            {
                this.logger.LogDebug("Out SearchAlertService.GetSubmittedAlertSearch");
            }

        }
        public IQueryable<SearchVM> FilterResult(string sort, string col, int pageNumber)
        {

            try
            {
                this.logger.LogDebug("In SearchServise.FilterResult");

                var submittedResult = from user in context.Users
                                      join uaddress in context.UserAddress
                                      on user.AddressId equals uaddress.AddressId
                                      select new SearchVM()
                                      {
                                          Id = user.UserId,
                                          Name = user.Name,
                                          Designation = user.Designation,
                                          JoiningDate = user.JoiningDate.ToShortDateString(),
                                          Image = user.ImagePath,
                                          Country = uaddress.Country,
                                          FullAddress = uaddress.Street + " " + uaddress.State + " " + uaddress.City + " " + uaddress.PinCode
                                      };
                var totalRecord = submittedResult.Count();
                submittedResult = Sorting(sort, col, submittedResult);
                submittedResult = PaginatedList<SearchVM>.CreateAsync(submittedResult.AsNoTracking(), pageNumber, 10).ToList().AsQueryable();

                if (submittedResult.Count() > 0)
                {
                    submittedResult.FirstOrDefault().TotalRecord = totalRecord.ToString();
                }
                return submittedResult;
            }
            catch (Exception e)
            {

                this.logger.LogDebug("In SearchServise.FilterResult Exception Message= " + e.Message);
                return new SearchVM[] { }.AsQueryable();
            }
            finally
            {
                this.logger.LogDebug("Out SearchServise.FilterResult");
            }

        }
        private static IQueryable<SearchVM> Sorting(string sorting, string column, IQueryable<SearchVM> query)
        {
            if (sorting == "desc")
            {
                if (column == "Id")
                {
                    query = query.OrderByDescending(x => x.Id);
                }
                if (column == "Name")
                {
                    query = query.OrderByDescending(x => x.Name);
                }
                if (column == "Designation")
                {
                    query = query.OrderByDescending(x => x.Designation);
                }
                if (column == "JoiningDate")
                {
                    query = query.OrderByDescending(x => x.JoiningDate);
                }
                if (column == "Country")
                {
                    query = query.OrderByDescending(x => x.Country);
                }
            }
            else
            {
                if (column == "Id")
                {
                    query = query.OrderBy(x => x.Id);
                }
                if (column == "Name")
                {
                    query = query.OrderBy(x => x.Name);
                }
                if (column == "Designation")
                {
                    query = query.OrderBy(x => x.Designation);
                }
                if (column == "JoiningDate")
                {
                    query = query.OrderBy(x => x.JoiningDate);
                }
                if (column == "Country")
                {
                    query = query.OrderBy(x => x.Country);
                }
            }

            return query;
        }
    }
}
