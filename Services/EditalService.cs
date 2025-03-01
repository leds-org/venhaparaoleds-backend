using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TrilhaBackendLeds.Dtos.ConcursoDtos;
using TrilhaBackendLeds.Dtos.EditaisDtos;
using TrilhaBackendLeds.Dtos.OrgaosDtos;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Services
{
    public class EditalService : IEditalService
    {
        private readonly IEditalRepository _editalRepository;
        private readonly IOrgaoRepository _orgaoRepository;
        private readonly IConcursoRepository _concursoRepository;

        public EditalService(IEditalRepository editalRepository, IOrgaoRepository orgaoRepository, IConcursoRepository concursoRepository)
        {
            _editalRepository = editalRepository;
            _orgaoRepository = orgaoRepository;
            _concursoRepository = concursoRepository;
        }

        public IEnumerable<EditalResponse> FindAll()
        {
            List<Edital> editais = _editalRepository.FindAll().ToList();

            List<EditalResponse> responseList = new List<EditalResponse>();

            if (editais.Any())
            {
                foreach (Edital edital in editais)
                {
                    var orgaoResponse = new OrgaoResponse(edital.Orgao.Id, edital.Orgao.Nome);
                    var concursoResponse = new ConcursoResponse(edital.Concurso.Id, edital.Concurso.CodigoDoConcurso);
                    responseList.Add(new EditalResponse(edital.Id, edital.NumeroDoEdital, orgaoResponse, concursoResponse));

                }
            }
            return responseList;
        }

        public EditalResponse FindById(int editalId)
        {
            Edital edital = _editalRepository.FindById(editalId);

            if (edital == null) throw new NotFoundException("Edital não encontrado");

            var orgaoResponse = new OrgaoResponse(edital.Orgao.Id, edital.Orgao.Nome);
            var concursoResponse = new ConcursoResponse(edital.Concurso.Id, edital.Concurso.CodigoDoConcurso);

            return new EditalResponse(edital.Id, edital.NumeroDoEdital, orgaoResponse, concursoResponse);
        }


        public EditalResponse FindByOrgaoIdAndNumeroDoEdital(int orgaoId, string numeroDoEdital)
        {
            Edital edital = _editalRepository.FindByOrgaoIdAndNumeroDoEdital(orgaoId, numeroDoEdital);

            if (edital == null) throw new NotFoundException("Edital não encontrado");

            var orgaoResponse = new OrgaoResponse(edital.Orgao.Id, edital.Orgao.Nome);
            var concursoResponse = new ConcursoResponse(edital.Concurso.Id, edital.Concurso.CodigoDoConcurso);

            return new EditalResponse(edital.Id, edital.NumeroDoEdital, orgaoResponse, concursoResponse);

        }

        public EditalResponse Create(EditalCreateRequest request)
        {
            var orgao = _orgaoRepository.FindOrgaoWithEditais(request.OrgaoId);
            var concurso = _concursoRepository.FindById(request.ConcursoId);

            ValidateEditalWhenCreating(request.NumeroDoEdital, orgao, concurso);

            Edital edital = _editalRepository.Create(new Edital(request.NumeroDoEdital, request.OrgaoId, request.ConcursoId));

            var orgaoResponse = new OrgaoResponse(orgao.Id, orgao.Nome);
            var concursoResponse = new ConcursoResponse(concurso.Id, concurso.CodigoDoConcurso);

            return new EditalResponse(edital.Id, edital.NumeroDoEdital, orgaoResponse, concursoResponse);

        }

        public EditalResponse Update(int editalId, EditalUpdateRequest updateRequest)
        {
            var edital = _editalRepository.FindById(editalId);

            if (edital == null) throw new NotFoundException("Edital não encontrado");

            edital = ApplyChangesToAnEdital(edital, updateRequest);

            edital = _editalRepository.Update(edital);

            var orgaoResponse = new OrgaoResponse(edital.Orgao.Id, edital.Orgao.Nome);
            var concursoResponse = new ConcursoResponse(edital.Concurso.Id, edital.Concurso.CodigoDoConcurso);

            return new EditalResponse(edital.Id, edital.NumeroDoEdital, orgaoResponse, concursoResponse);
        }

        public void DeleteById(int editalId)
        {
            Edital edital = _editalRepository.FindById(editalId);

            if (edital == null) throw new NotFoundException("Edital não encontrado");

            _editalRepository.DeleteById(editalId);
        }

        public Edital ApplyChangesToAnEdital(Edital edital, EditalUpdateRequest request)
        {
            bool isNumeroDoEditalAlterado = edital.NumeroDoEdital != request.NumeroDoEdital && request.NumeroDoEdital != null;
            bool isOrgaoAlterado = edital.OrgaoId != request.OrgaoId && request.OrgaoId != null;
            bool isConcursoAlterado = edital.ConcursoId != request.ConcursoId && request.ConcursoId != null;

            bool hasChanges = isNumeroDoEditalAlterado || isOrgaoAlterado || isConcursoAlterado;

            if (!hasChanges) throw new BadRequestException("Pelo menos uma alteração deve ser fornecida");


            Orgao orgao;

            if (isOrgaoAlterado)
            {
                orgao = _orgaoRepository.FindOrgaoWithEditais(request.OrgaoId.Value);
                ValidateIfOrgaoExists(orgao);
                edital.Orgao = orgao;
            }
            else
            {
                orgao = edital.Orgao;
            }

            if (isNumeroDoEditalAlterado)
            {
                ValidateNumeroDoEdital(request.NumeroDoEdital, orgao);
                edital.NumeroDoEdital = request.NumeroDoEdital;
            }

            if (isConcursoAlterado)
            {
                var concurso = _concursoRepository.FindById(request.ConcursoId.Value);
                ValidateIfConcursoExists(concurso);
                edital.NumeroDoEdital = request.NumeroDoEdital;
            }

            return edital;
        }

        public void ValidateEditalWhenCreating(string numeroDoEdital, Orgao orgao, Concurso concurso)
        {
            ValidateNumeroDoEdital(numeroDoEdital, orgao);
            ValidateIfOrgaoExists(orgao);
            ValidateIfConcursoExists(concurso);  
        }

        public void ValidateNumeroDoEdital(string numeroDoEdital, Orgao orgao)
        {
            if (!Regex.IsMatch(numeroDoEdital, @"^\d{1,4}/\d{4}$"))
                throw new BadRequestException("Formato do número do edital inválido. Use o formato N/AAAA, onde N pode ter até 4 dígitos.");

            foreach (Edital e in orgao.Editais)
            {
                if (e.NumeroDoEdital == numeroDoEdital) throw new BadRequestException("Esse orgão já possui esse número do edital");
            }
        }

        public void ValidateIfOrgaoExists(Orgao orgao)
        {
            if (orgao == null) throw new BadRequestException("Orgão não encontrado");
        }

        public void ValidateIfConcursoExists(Concurso concurso)
        {
            if (concurso == null) throw new BadRequestException("Concurso não encontrado");
        }

    }
}
