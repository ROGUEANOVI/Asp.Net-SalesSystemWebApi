using AutoMapper;
using SalesSystemWebApi.DTO;
using SalesSystemWebApi.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.Utility
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region User
            CreateMap<User, UserDTO>()
                .ForMember(destination =>

                    destination.RolDescription,
                    option => option.MapFrom(origin => origin.Rol.Name)
                )
                .ForMember(destination =>
                    destination.IsActive,
                    option => option.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<User, SessionDTO>()
                .ForMember(destination =>

                    destination.RolDescription,
                    option => option.MapFrom(origin => origin.Rol.Name)
                );

            CreateMap<UserDTO, User>()
                .ForMember(destination =>
                    destination.Rol,
                    option => option.Ignore()
                )
                .ForMember(destination =>
                    destination.IsActive,
                    option => option.MapFrom(origin => origin.IsActive == 1 ? true : false)
                ); ;
            #endregion

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(destination =>
                    destination.CategoryDescription,
                    option => option.MapFrom(origin => origin.Category.Name)
                )
                .ForMember(destination =>
                    destination.Price,
                    option => option.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                 .ForMember(destination =>
                    destination.IsActive,
                    option => option.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<ProductDTO, Product>()
                .ForMember(destination =>
                    destination.Category,
                    option => option.Ignore()
                )
                .ForMember(destination =>
                    destination.Price,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-CO")))
                )
                 .ForMember(destination =>
                    destination.IsActive,
                    option => option.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );
            #endregion

            #region Sale
            CreateMap<Sale, SaleDTO>()
                .ForMember(destination =>
                    destination.Total,
                    option => option.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.RegistrationDate,
                    option => option.MapFrom(origin => origin.RegistrationDate.Value.ToString("dd/MM/yy"))
                );

            CreateMap<SaleDTO, Sale>()
                .ForMember(destination =>
                    destination.Total,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("es-CO")))
                );
            #endregion

            #region SaleDetail
            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(destination =>

                    destination.ProductDescription,
                    option => option.MapFrom(origin => origin.Product.Name)
                )
                .ForMember(destination =>
                    destination.Price,
                    option => option.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.Total,
                    option => option.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
                );

            CreateMap<SaleDetailDTO, SaleDetail>()
                 .ForMember(destination =>
                    destination.Price,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-CO")))
                )
                  .ForMember(destination =>
                    destination.Total,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("es-CO")))
                );

            #endregion

            #region Report
            CreateMap<SaleDetail, ReportDTO>()
                .ForMember(destination =>
                    destination.RegistrationDate,
                    option => option.MapFrom(origin => origin.Sale.RegistrationDate.Value.ToString("dd/MM/yy"))
                )
                .ForMember(destination =>
                    destination.TicketNumber,
                    option => option.MapFrom(origin => origin.Sale.SaleTicketNumber)
                )
                .ForMember(destination =>
                    destination.PaymentType,
                    option => option.MapFrom(origin => origin.Sale.PaymentType)
                )
                .ForMember(destination =>
                    destination.SaleTotal,
                    option => option.MapFrom(origin => Convert.ToString(origin.Sale.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.Product,
                    option => option.MapFrom(origin => origin.Product.Name)
                )
                .ForMember(destination =>
                    destination.Price,
                    option => option.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destination =>
                    destination.Total,
                    option => option.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
                );
            #endregion

        }
    }
}
