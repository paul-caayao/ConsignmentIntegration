using AutoMapper;
using ConsignmentIntegration.Logic.DTO;
using ConsignmentIntegration.Models;

namespace ConsignmentIntegration.Logic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Consignment, ConsignmentRequest>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.SenderDetails.Name))
                .ForMember(dest => dest.SenderAddress, opt => opt.MapFrom(src => src.SenderDetails.Address))
                .ForMember(dest => dest.SenderAddress2, opt => opt.MapFrom(src => src.SenderDetails.Address2))
                .ForMember(dest => dest.SenderSuburb, opt => opt.MapFrom(src => src.SenderDetails.Suburb))
                .ForMember(dest => dest.SenderState, opt => opt.MapFrom(src => src.SenderDetails.State))
                .ForMember(dest => dest.SenderPostcode, opt => opt.MapFrom(src => src.SenderDetails.Postcode))
                .ForMember(dest => dest.SenderReference, opt => opt.MapFrom(src => src.SenderDetails.Reference))
                .ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.SenderDetails.Email))
                .ForMember(dest => dest.ConsignmentSenderContact, opt => opt.MapFrom(src => src.SenderDetails.Contact))
                .ForMember(dest => dest.ConsignmentSenderPhone, opt => opt.MapFrom(src => src.SenderDetails.Phone))
                .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.ReceiverDetails.Name))
                .ForMember(dest => dest.ReceiverAddress, opt => opt.MapFrom(src => src.ReceiverDetails.Address))
                .ForMember(dest => dest.ReceiverAddress2, opt => opt.MapFrom(src => src.ReceiverDetails.Address2))
                .ForMember(dest => dest.ReceiverSuburb, opt => opt.MapFrom(src => src.ReceiverDetails.Suburb))
                .ForMember(dest => dest.ReceiverState, opt => opt.MapFrom(src => src.ReceiverDetails.State))
                .ForMember(dest => dest.ReceiverPostcode, opt => opt.MapFrom(src => src.ReceiverDetails.Postcode))
                .ForMember(dest => dest.ReceiverEmail, opt => opt.MapFrom(src => src.ReceiverDetails.Email))
                .ForMember(dest => dest.ConsignmentReceiverContact, opt => opt.MapFrom(src => src.ReceiverDetails.Contact))
                .ForMember(dest => dest.ConsignmentReceiverPhone, opt => opt.MapFrom(src => src.ReceiverDetails.Phone))
                .ForMember(dest => dest.ConsignmentReceiverIsResidential, opt => opt.MapFrom(src => src.ReceiverIsResidential))
                .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Rows))
                .ForMember(dest => dest.ReturnPdfLabels, opt =>
                    opt.MapFrom(src => src.ReturnPdfLabels ? "y" : null)) 
                .ForMember(dest => dest.ReturnPdfConsignment, opt =>
                    opt.MapFrom(src => src.ReturnPdfConsignment ? "y" : null));

            CreateMap<Row, RowResponse>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Barcodes.Select(barcode => new ItemResponse { Barcode = barcode }).ToList()));

        }
    }
}
