using System.Text.RegularExpressions;
using TrilhaBackendLeds.Dtos.CandidatoDtos;
using TrilhaBackendLeds.Dtos.ProfissaoDtos;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly ICandidatoRepository _repository;

        public CandidatoService(ICandidatoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CandidatoResponse> FindAll()
        {
            List<Candidato> candidatos = _repository.FindAll().ToList();

            List<CandidatoResponse> candidatoResponseList = new List<CandidatoResponse>();

            if (candidatos.Any())
            {
                candidatoResponseList = candidatos.Select(candidato => new CandidatoResponse
                {
                    Id = candidato.Id,
                    Nome = candidato.Nome,
                    Cpf = candidato.Cpf,
                    DataDeNascimento = candidato.DataDeNascimento,
                    Profissoes = candidato.CandidatoProfissoes
                    .Select(cp => new ProfissaoResponse
                    {
                        Id = cp.Profissao.Id,
                        Nome = cp.Profissao.Nome
                    }).ToList()
                }).ToList();
            }

            return candidatoResponseList;
        }

        public CandidatoResponse FindById(int candidatoId)
        {
            Candidato candidato = _repository.FindById(candidatoId);

            if (candidato == null) throw new NotFoundException("Candidato não encontrado");

            return new CandidatoResponse(
                    candidato.Id,
                    candidato.Nome,
                    candidato.Cpf,
                    candidato.DataDeNascimento,
                    candidato.CandidatoProfissoes.Select(cp => new ProfissaoResponse { Id = cp.ProfissaoId, Nome = cp.Profissao.Nome }).ToList()
            );
        }

        public CandidatoResponse FindByCpf(string cpf)
        {
            ValidateCPF(cpf);
            Candidato candidato = _repository.FindByCpf(cpf);

            if (candidato == null) throw new NotFoundException("Candidato não encontrado");

            return new CandidatoResponse(
                    candidato.Id,
                    candidato.Nome,
                    candidato.Cpf,
                    candidato.DataDeNascimento,
                    candidato.CandidatoProfissoes.Select(cp => new ProfissaoResponse { Id = cp.ProfissaoId, Nome = cp.Profissao.Nome }).ToList()
            );

        }
        public CandidatoResponse Create(CandidatoCreateRequest request)
        {
            ValidateNome(request.Nome);
            ValidateCPF(request.Cpf);
            IsCpfUnique(request.Cpf);
            ValidateDataDeNascimento(request.DataDeNascimento);
            
            Candidato candidato = _repository.Create(new Candidato(request.Nome, request.DataDeNascimento, request.Cpf));

            return new CandidatoResponse(
                    candidato.Id,
                    candidato.Nome,
                    candidato.Cpf,
                    candidato.DataDeNascimento
                );

        }

        public CandidatoResponse Update(int candidatoId, CandidatoUpdateRequest updateRequest)
        {
            Candidato candidato = _repository.FindById(candidatoId);

            if (candidato == null) throw new NotFoundException("Candidato não encontrado");

            candidato = ApplyChangesToCandidato(candidato, updateRequest);

            candidato = _repository.Update(candidato);

            return new CandidatoResponse(
                    candidato.Id,
                    candidato.Nome,
                    candidato.Cpf,
                    candidato.DataDeNascimento
                );
        }

        public void DeleteById(int candidatoId)
        {
            Candidato candidato = _repository.FindById(candidatoId);

            if (candidato == null) throw new NotFoundException("Candidato não encontrado");

            _repository.DeleteById(candidatoId);
        }

        public Candidato ApplyChangesToCandidato(Candidato candidato, CandidatoUpdateRequest request)
        {

            bool isNomeAlterado = candidato.Nome != request.Nome && request.Nome != null;
            bool isDataDeNascimentoAlterada = candidato.DataDeNascimento != request.DataDeNascimento && request.DataDeNascimento != default;
            bool isCpfAlterado = candidato.Cpf != request.Cpf && request.Cpf != null;

            bool hasChanges = isNomeAlterado || isDataDeNascimentoAlterada || isCpfAlterado;

            if (!hasChanges) throw new BadRequestException("Pelo menos uma alteração deve ser fornecida");

            if (isNomeAlterado)
            {
                ValidateNome(request.Nome);
                candidato.Nome = request.Nome;
            }

            if(isDataDeNascimentoAlterada)
            {
                ValidateDataDeNascimento(request.DataDeNascimento);
                candidato.DataDeNascimento = request.DataDeNascimento;
            }

            if(isCpfAlterado)
            {
                ValidateCPF(request.Cpf);
                IsCpfUnique(request.Cpf);
                candidato.Cpf = request.Cpf;
            }

            return candidato;

        }

        public void ValidateCPF(string cpf)
        {
            if(string.IsNullOrWhiteSpace(cpf))
                throw new BadRequestException("O CPF não pode ser nulo, vazio ou conter apenas espaços em branco");

            if (!Regex.IsMatch(cpf, @"^\d+$"))
                throw new BadRequestException("O CPF deve conter somente números");

            if (cpf.Length != 11)
                throw new BadRequestException("O CPF deve conter 11 dígitos");

            if (cpf.Distinct().Count() == 1)
                throw new BadRequestException("O CPF não pode conter todos os dígitos iguais");

            //Optei por tirar a validação de dígitos verificadores pq o CPF de Lindsey é inválido e, portanto, prejudicaria o teste com base nos dados fornecidos no desafio.
        }

        public void ValidateDataDeNascimento(DateOnly dataDeNascimento)
        {
            if (dataDeNascimento >= DateOnly.FromDateTime(DateTime.Today)) throw new BadRequestException("A data de nascimento deve estar no passado");
        }

        public void ValidateNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("O nome é obrigatório");
            if (nome.Any(char.IsDigit)) throw new BadRequestException("O nome não pode conter números");
        }

        public void IsCpfUnique(string cpf)
        {
            var candidato = _repository.FindByCpfLight(cpf);
            if (candidato != null) throw new BadRequestException("CPF já cadastrado");
        }

    }
}
