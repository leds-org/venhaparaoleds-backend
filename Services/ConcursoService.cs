
using TrilhaBackendLeds.Dtos.ConcursoDtos;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Services
{
    public class ConcursoService : IConcursoService
    {
        private readonly IConcursoRepository _repository;

        public ConcursoService(IConcursoRepository repository)
        {
            this._repository = repository;
        }

        public IEnumerable<ConcursoResponse> FindAll()
        {
            List<Concurso> concurso = _repository.FindAll().ToList();

            List<ConcursoResponse> responseList = new List<ConcursoResponse>();

            if (concurso.Any())
            {
                concurso.ForEach(concurso => { responseList.Add(new ConcursoResponse(concurso.Id, concurso.CodigoDoConcurso)); });
            }

            return responseList;
        }

        public ConcursoResponse FindById(int concursoId)
        {
            Concurso concurso = _repository.FindById(concursoId);

            if (concurso == null) throw new NotFoundException("Concurso não encontrado");

            return new ConcursoResponse(concurso.Id, concurso.CodigoDoConcurso);
        }

        public ConcursoResponse FindByCodigoDoConcurso(string codigoDoConcurso)
        {
            ValidateCodigoDoConcurso(codigoDoConcurso);
            Concurso concurso = _repository.FindByCodigoDoConcurso(codigoDoConcurso);
            if (concurso == null) throw new NotFoundException("Concurso não encontrado");
            return new ConcursoResponse(concurso.Id, concurso.CodigoDoConcurso);
        }

        public ConcursoResponse Create(ConcursoCreateRequest request)
        {
            ValidateCodigoDoConcurso(request.CodigoDoConcurso);

            IsCodigoDoConcursoUnique(request.CodigoDoConcurso);

            Concurso concurso = _repository.Create(new Concurso(request.CodigoDoConcurso));

            return new ConcursoResponse(concurso.Id, concurso.CodigoDoConcurso);

        }

        public ConcursoResponse Update(int concursoId, ConcursoUpdateRequest request)
        {
            Concurso concurso = _repository.FindById(concursoId);

            if (concurso == null) throw new NotFoundException("concurso não encontrado");

            concurso = ApplyChangesToConcurso(concurso, request);

            concurso = _repository.Update(concurso);

            return new ConcursoResponse(concurso.Id, concurso.CodigoDoConcurso);
        }

        public void DeleteById(int concursoId)
        {
            Concurso concurso = _repository.FindById(concursoId);

            if (concurso == null) throw new NotFoundException("concurso não encontrado");

            _repository.DeleteById(concursoId);
        }

        public Concurso ApplyChangesToConcurso(Concurso concurso, ConcursoUpdateRequest request)
        {
            bool isCodigoDoConcursoAlterado = concurso.CodigoDoConcurso != request.CodigoDoConcurso && request.CodigoDoConcurso != null;

            bool hasChanges = isCodigoDoConcursoAlterado;

            if (!hasChanges) throw new BadRequestException("Pelo menos uma alteração deve ser fornecida");

            if (isCodigoDoConcursoAlterado)
            {
                ValidateCodigoDoConcurso(request.CodigoDoConcurso);
                IsCodigoDoConcursoUnique(request.CodigoDoConcurso);
                concurso.CodigoDoConcurso = request.CodigoDoConcurso;
            }

            return concurso;
        }
        
        public void ValidateCodigoDoConcurso(string codigoDoConcurso)
        {
            if (codigoDoConcurso.Length != 11)
                throw new BadRequestException("O Código do concurso deve conter 11 números");

            if (string.IsNullOrWhiteSpace(codigoDoConcurso))
                throw new BadRequestException("O código do concurso não pode ser nulo, vazio ou conter apenas espaços em branco");

            if (!codigoDoConcurso.All(char.IsDigit))
                throw new BadRequestException("O código do concurso deve conter apenas números");
        }

        public void IsCodigoDoConcursoUnique(string codigoDoConcurso)
        {
            var concurso =_repository.FindByCodigoDoConcurso(codigoDoConcurso);
            if (concurso != null) throw new BadRequestException("O código do concurso já existe");
        }
    }
}
