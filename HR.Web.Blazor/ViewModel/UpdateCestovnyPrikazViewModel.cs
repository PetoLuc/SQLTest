using HR.Dal.MsSql.Repos.Contracts;
using HR.Dal.Repos.Contracts;
using HR.Dol;
using Microsoft.AspNetCore.Mvc;

namespace HR.Web.Blazor.ViewModel
{
    public class UpdateCestovnyPrikazViewModel(IDopravaTypRepository dopravaTypRepository,                                      
                                      IMestoRepository mestoRepository,
                                      IStavRepository stavRepository,
                                      IZamestnanecRepository zamestnanecRepository,
                                      IDopravaRepository dopravaRepository,
                                      ICestovnyPrikazRepository cestovnyPrikazRepository)
    {        

        #region Model
        [BindProperty]
        public DateTime DatumCasZaciatku { get; set; }

        [BindProperty]
        public DateTime DatumCasKonca { get; set; }

        [BindProperty]
        public int? MiestoZaciatkuMestoId { get; set; }

        [BindProperty]
        public int? MiestoKoncaMestoId { get; set; }


        [BindProperty]
        public int DopravaId { get; set; }

        [BindProperty]
        public int StavId { get; set; }

        [BindProperty]
        public string? ZamestnanecId { get; set; }

        public List<DopravaTyp> DopravaAddList { get; private set; } = [];

        public List<DopravaTyp> DopravaTypeList { get; private set; } = [];

        public List<Zamestnanec> ZamestnanecList { get; private set; } = [];

        public List<Stav> StavList { get; private set; } = [];

        public List<Mesto> MestoList { get; private set; } = [];
        public int CestovnyPrikazId { get; set; }

        #endregion        

        public async Task InitAsync()
        {            
            var foundCestovnyPrikaz = await cestovnyPrikazRepository.GetByIdAsync(CestovnyPrikazId) ?? throw new InvalidOperationException($"TODO cestovny prikaz not foud by {CestovnyPrikazId}");            

            MestoList = await mestoRepository.GetAllMestoForSelectAsync();
            ZamestnanecList = await zamestnanecRepository.GetAllZamestnanciAsync();
            DopravaId =   DopravaTyp.SluzobneAuto.DopravaTypId;
            StavId = foundCestovnyPrikaz.StavId;
            MiestoZaciatkuMestoId = foundCestovnyPrikaz.MiestoZaciatkuId;
            MiestoKoncaMestoId = foundCestovnyPrikaz.MiestoKoncaId;
            ZamestnanecId =foundCestovnyPrikaz.UcastnikId;
            var currDopravaList = await dopravaRepository.GetDopravaByCestovnyPrikazIdAsync(foundCestovnyPrikaz.CpId);
            DopravaAddList = currDopravaList.Select(d => dopravaTypRepository.FindById(d.DopravaTypId)).ToList();
            StavList = stavRepository.GetAll();            
            DopravaTypeList = [.. dopravaTypRepository.GetAll()];            
        }

        public void AddDoprava()
        {
            var dopravaType = DopravaTypeList.FirstOrDefault(td => td.DopravaTypId == DopravaId);
            if (dopravaType != null)
            {
                DopravaAddList.Add(dopravaType);
            }
        }
        public void RemoveDoprava()
        {
            if (DopravaAddList.Count > 0)
            {
                DopravaAddList.RemoveAt(DopravaAddList.Count - 1);
            }            
        }

        public async Task SaveAsync()
        {
            if (ZamestnanecId != null && MiestoKoncaMestoId !=null && MiestoZaciatkuMestoId!=null)
            {
                await cestovnyPrikazRepository.UpdateAsync(new CestovnyPrikaz
                {
                    CpId = CestovnyPrikazId,
                    UcastnikId = ZamestnanecId!,
                    DatumCasKonca = DatumCasKonca,
                    DatumCasZaciatku = DatumCasZaciatku,
                    MiestoKoncaId = MiestoKoncaMestoId.Value,
                    MiestoZaciatkuId = MiestoZaciatkuMestoId.Value,
                    StavId = StavId
                }, DopravaAddList);
            }
        }
    }
}
