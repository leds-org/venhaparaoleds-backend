using TrilhaBackendLeds.Dtos.ProfissaoDtos;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;
using TrilhaBackendLeds.Exceptions;

namespace TrilhaBackendLeds.Services
{
    public class ProfissaoService : IProfissaoService
    {
        private readonly IProfissaoRepository _repository;

        public ProfissaoService(IProfissaoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProfissaoResponse> FindAll()
        {
            List<Profissao> profissoes = _repository.FindAll().ToList();

            List<ProfissaoResponse> responseList = new List<ProfissaoResponse>();

            if (profissoes.Any())
            {
                profissoes.ForEach(profissao => { responseList.Add(new ProfissaoResponse(profissao.Id, profissao.Nome)); });
            }

            return responseList;
        }

        public ProfissaoResponse FindById(int profissaoId)
        {
            Profissao profissao = _repository.FindById(profissaoId);

            if (profissao == null) throw new NotFoundException("Profissão não encontrada");

            return new ProfissaoResponse(profissao.Id, profissao.Nome);
        }

        public ProfissaoResponse Create(ProfissaoCreateRequest request)
        {
            ValidateNomeDaProfissao(request.Nome);

            Profissao profissao = _repository.Create(new Profissao(request.Nome));

            return new ProfissaoResponse(profissao.Id, profissao.Nome);

        }

        public ProfissaoResponse Update(int profissaoId, ProfissaoUpdateRequest updateRequest)
        {
            Profissao profissao = _repository.FindById(profissaoId);

            if (profissao == null) throw new NotFoundException("Profissão não encontrada");

            ValidateNomeDaProfissao(updateRequest.Nome);

            profissao.Nome = updateRequest.Nome;

            profissao = _repository.Update(profissao);

            return new ProfissaoResponse(profissao.Id, profissao.Nome);
        }

        public void DeleteById(int profissaoId)
        {
            Profissao profissao = _repository.FindById(profissaoId);

            if (profissao == null) throw new NotFoundException("Profissão não encontrada");

            _repository.DeleteById(profissaoId);
        }

        public void ValidateNomeDaProfissao(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("O nome da profissão não pode ser nulo ou vazio");

            if (nome != nome.ToLower()) throw new BadRequestException("O nome da profissão deve estar em letras minúsculas");

            var profissao = _repository.FindByNome(nome);

            if (profissao != null) throw new BadRequestException("Já existe uma profissão com esse nome");
        }

    }
}
