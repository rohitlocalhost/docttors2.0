using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;
using System.Web.Routing;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.DataAccess.Repositories;
using Docttors_portal.DataAccess.EntityModel;
using Docttors_portal.Services.Interfaces;
using Docttors_portal.Services.Classes;

namespace Docttors_portal.DependencyResolution.DependencyResolution
{
    public class DependencyRegistrar : DefaultControllerFactory
    {
        // private readonly string _controllerName = "LoginController";//Show Default Controllerwhich willbe used when don't find controller

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if ((requestContext == null) || (controllerType == null))
                return null;
            return (Controller)ObjectFactory.GetInstance(controllerType);
        }

        /// <summary>
        /// Used for Dependency Injection
        /// </summary>
        /// <CreatedBy>Rohit Singh Airi</CreatedBy>
        /// <CreatedDate>24thFeb2023</CreatedDate>
        public static class StructureMapper
        {
            public static void Run()
            {
                ControllerBuilder.Current.SetControllerFactory(new DependencyRegistrar());

                ObjectFactory.Initialize(action =>
                {
                    action.AddRegistry(new RepositoryRegistry());
                });
            }
        }

        /// <summary>        
        /// To Map Interface with Repository
        /// </summary>
        /// <CreatedBy>Rohit Singh Airi</CreatedBy>
        /// <CreatedDate>24thFeb2023</CreatedDate>
        public class RepositoryRegistry : Registry
        {
            public RepositoryRegistry()
            {
                For<IUnitOfWork>().Use<UnitOfWork<DocttorsEntities>>();
                For<IUserLogOnService>().Use<UserLogOnService>();
                //For<IAddressBookService>().Use<AddressBookService>();
                For<ICommonUtilityService>().Use<CommonUtilityService>();
                //For<IUserService>().Use<UserService>();
                //For<IExpenseService>().Use<ExpenseService>();
                //For<IProjectService>().Use<ProjectServices>();
                //For<IInvoiceService>().Use<InvoiceService>();
                //For<IAdminService>().Use<AdminService>();
                //For<IApplicationPageService>().Use<ApplicationPageService>();
                //For<IEmailTemplateService>().Use<EmailTemplateService>();
                //For<ISettingService>().Use<SettingService>();
                //For<IEmailTemplateSubstitutionService>().Use<EmailTemplateSubstitutionService>();
                //For<IBankingService>().Use<BankingService>();
                //For<IPayYourselfService>().Use<PayYourselfService>();
                //For<ITaxPaymentService>().Use<TaxPaymentService>();
                //For<IReportService>().Use<ReportService>();
            }
        }
    }
}
