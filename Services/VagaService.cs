
using TrilhaBackendLeds.Dtos.VagasDtos;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Services
{
    public class VagaService : IVagasService
    {
        private readonly IVagasRepository _repository;
        private readonly IProfissaoService _profissaoService;
        private readonly IEditalService _editalService;

        public VagaService(IVagasRepository repository, IProfissaoService profissaoService, IEditalService editalService)
        {
            _repository = repository;
            _profissaoService = profissaoService;
            _editalService = editalService;
        }

        public IEnumerable<VagaResponse> FindAll()
        {
            List<Vaga> vagas = _repository.FindAll().ToList();

            List<VagaResponse> responseList = new List<VagaResponse>();

            if (vagas.Any())
            {
                foreach (var vaga in vagas)
                {
                    responseList.Add(new VagaResponse(vaga.Id, vaga.Profissao.Nome, vaga.Edital.NumeroDoEdital, vaga.Edital.Concurso.CodigoDoConcurso));
                }
            }

            return responseList;
        }

        public VagaResponse FindById(int vagaId)
        {
            Vaga vaga = _repository.FindById(vagaId);

            if (vaga == null) throw new NotFoundException("Vaga não encontrada");

            return new VagaResponse(vaga.Id, vaga.Profissao.Nome, vaga.Edital.NumeroDoEdital, vaga.Edital.Concurso.CodigoDoConcurso);
        }

        public VagaResponse Create(VagaCreateRequest request)
        {
            ValidateIfVagaExists(request.ProfissaoId, request.EditalId);
            ValidateIfProfissaoExists(request.ProfissaoId);
            ValidateIfEditalExists(request.EditalId);

            Vaga vaga = _repository.Create(new Vaga(request.ProfissaoId, request.EditalId));

            return new VagaResponse(vaga.Id, vaga.Profissao.Nome, vaga.Edital.NumeroDoEdital, vaga.Edital.Concurso.CodigoDoConcurso);
        }

        public VagaResponse Update(int vagaId, VagaUpdateRequest updateRequest)
        {

            Vaga vaga = _repository.FindById(vagaId);

            if (vaga == null) throw new NotFoundException("Vaga não encontrada");

            vaga = ApplyUpdatesToAVaga(vaga, updateRequest);

            return new VagaResponse(vaga.Id, vaga.Profissao.Nome, vaga.Edital.NumeroDoEdital, vaga.Edital.Concurso.CodigoDoConcurso);
        }

        public void DeleteById(int vagaId)
        {
            Vaga vaga = _repository.FindById(vagaId);

            if (vaga == null) throw new NotFoundException("Vaga não encontrada");

            _repository.DeleteById(vagaId);
        }

        public Vaga ApplyUpdatesToAVaga(Vaga vaga, VagaUpdateRequest updateRequest) 
        {
            bool isProfissaoIdChanged = vaga.ProfissaoId != updateRequest.ProfissaoId && updateRequest.ProfissaoId != null;
            bool isEditalIdChanged = vaga.EditalId != updateRequest.EditalId && updateRequest.EditalId != null;

            bool hasChanges = isEditalIdChanged || isEditalIdChanged;

            if (!hasChanges) throw new BadRequestException("Pelo menos uma alteração deve ser fornecida");

            if (isProfissaoIdChanged)
            {
                ValidateIfProfissaoExists(updateRequest.ProfissaoId.Value);
                vaga.ProfissaoId = updateRequest.ProfissaoId.Value;
            }

            if (isEditalIdChanged)
            {
                ValidateIfEditalExists(updateRequest.EditalId.Value);
                vaga.EditalId = updateRequest.EditalId.Value;
            }

            ValidateIfVagaExists(vaga.ProfissaoId, vaga.EditalId);

            return _repository.Update(vaga);
        }

        public void ValidateIfVagaExists(int profissaoId, int EditalId)
        {
            var vaga = _repository.FindByProfissaoIdAndEditalId(profissaoId, EditalId);
            if (vaga != null) throw new BadRequestException("A vaga já existe");
        }

        public void ValidateIfProfissaoExists(int profissaoId)
        {
            var profissao = _profissaoService.FindById(profissaoId);
            if(profissao == null) throw new NotFoundException("A profissão não existe");
        }

        public void ValidateIfEditalExists(int EditalId)
        {
            var edital = _editalService.FindById(EditalId);
            if (edital == null) throw new NotFoundException("O Edital não existe");
        }
    }
}
