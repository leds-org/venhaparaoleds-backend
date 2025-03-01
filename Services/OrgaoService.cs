using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using TrilhaBackendLeds.Dtos.OrgaosDtos;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Services
{
    public class OrgaoService : IOrgaoService
    {
        private readonly IOrgaoRepository _repository;

        public OrgaoService(IOrgaoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrgaoResponse> FindAll()
        {
            List<Orgao> orgaos = _repository.FindAll().ToList();

            List<OrgaoResponse> responseList = new List<OrgaoResponse>();

            if (orgaos.Any())
            {
                orgaos.ForEach(orgao => { responseList.Add(new OrgaoResponse(orgao.Id, orgao.Nome)); });
            }

            return responseList;
        }

        public OrgaoResponse FindById(int orgaoId)
        {
            Orgao orgao = _repository.FindById(orgaoId);

            if (orgao == null) throw new NotFoundException("Órgao não encontrado");

            return new OrgaoResponse(orgao.Id, orgao.Nome);
        }

        public OrgaoResponse FindByNome(string nomeDoOrgao)
        {
            Orgao orgao = _repository.FindByNome(nomeDoOrgao);
            if (orgao == null) throw new NotFoundException("Órgao não encontrado");
            return new OrgaoResponse(orgao.Id, orgao.Nome);
        }

        public OrgaoResponse Create(OrgaoCreateRequest request)
        {
            ValidateNomeDoOrgao(request.Nome);
            Orgao orgao = _repository.Create(new Orgao(request.Nome));

            return new OrgaoResponse(orgao.Id, orgao.Nome);
        }

        public OrgaoResponse Update(int orgaoId, OrgaoUpdateRequest updateRequest)
        {
            Orgao orgao = _repository.FindById(orgaoId);

            if (orgao == null) throw new NotFoundException("Órgao não encontrado");

            orgao = ApplyChangesToAnOrgao(orgao, updateRequest);

            orgao = _repository.Update(orgao);

            return new OrgaoResponse(orgao.Id, orgao.Nome);
        }

        public void DeleteById(int orgaoId)
        {
            Orgao orgao = _repository.FindById(orgaoId);

            if (orgao == null) throw new NotFoundException("Órgao não encontrado");

            _repository.DeleteById(orgaoId);
        }

        public Orgao ApplyChangesToAnOrgao(Orgao orgao, OrgaoUpdateRequest request)
        {
            bool isNomeDoOrgaoAlterado = orgao.Nome != request.Nome && request.Nome != null;

            bool hasChanges = isNomeDoOrgaoAlterado;

            if (!hasChanges) throw new BadRequestException("Pelo menos uma alteração deve ser fornecida");

            if (isNomeDoOrgaoAlterado)
            {
                ValidateNomeDoOrgao(request.Nome);
                orgao.Nome = request.Nome;
            }

            return orgao;
        }

        public void ValidateNomeDoOrgao(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("O nome do órgão não pode ser nulo ou vazio");

            if (nome != nome.ToUpper()) throw new BadRequestException("O nome do órgão deve estar em letras maiúsculas");

            var orgao = _repository.FindByNome(nome);

            if (orgao != null) throw new BadRequestException("Já existe um órgão com esse nome");
        }

        
    }
}
