using AppCommonMethods;
using CommonMessages;
using HMIS.Patient.Domain.Models.HMIS_HUBModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Service
{
    public class DataBankService
    {
        public async Task<List<Person>> GetPatientFromDataBank(string SearchKey, string SearchValue)
        {
            HmisHubContext _db = new HmisHubContext();
            SearchValue = SearchValue.Replace("-", "");
            //var patientList = await _db.People.Where(x => (x.Cnic.Replace("-", "") == input.Replace("-", "") || x.MobileNumber.Replace("-","") == input.Replace("-", "")) && x.IsRegisteredHmis == false).ToListAsync();

            var conn = _db.Database.GetDbConnection();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("FilterPatient", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                //if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                sqlComm.Parameters.AddWithValue("@searchType", SearchKey);
                sqlComm.Parameters.AddWithValue("@searchValue", SearchValue);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                await Task.Run(() => da.Fill(ds));
                List<Person> lst = ds.Tables[0].ToList<Person>();
                return lst;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            //return patientList;
        }

        public async Task UpdateHMISFlagWithId(int? input)
        {
            HmisHubContext _db = new HmisHubContext();

            var obj = await _db.People.Where(x => x.Id == input).FirstOrDefaultAsync();

            if (obj != null)
            {
                obj.IsRegisteredHmis = true;
                _db.Update(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateHMISFlagWithCnic(string? cnic)
        {
            HmisHubContext _db = new HmisHubContext();

            cnic = cnic.Replace("-", "");

            var objList = await _db.People.Where(x => x.Cnic == cnic && (
            x.CnicrelationName!.ToLower() == CommonStringConstant.RelationSelf.ToLower()
            || x.CnicrelationName! == "" || x.CnicrelationName! == null)
            ).ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList(objList))
            {
                foreach (var item in objList)
                {
                    item.IsRegisteredHmis = true;
                    _db.Update(item);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
