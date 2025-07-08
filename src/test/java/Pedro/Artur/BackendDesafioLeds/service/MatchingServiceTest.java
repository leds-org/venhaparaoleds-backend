package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.dtos.CandidatoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.dtos.ConcursoResponseDTO;
import Pedro.Artur.BackendDesafioLeds.exception.NoMatchCandidatoException;
import Pedro.Artur.BackendDesafioLeds.exception.NoMatchConcursoException;
import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;
import java.util.Set;

import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.when;

class MatchingServiceTest {
    @Mock
    ConcursoService concursoService;

    @Mock
    CandidatoService candidatoService;

    @InjectMocks
    @Autowired
    MatchingService matchingService;

    @BeforeEach
    void setup(){
        MockitoAnnotations.initMocks(this);
    }

    @Test
    @DisplayName("Deve retornar com sucesso concursos compativeis")
    void buscarConcursosCompativeisPorCpfCase1() {
        String cpf = "12332132112";
        Candidato candidato = new Candidato(1L, "Joao", cpf,"13/04/2001",List.of("advogado","policial"));

        ConcursoResponseDTO concurso1 = new ConcursoResponseDTO("SEJUS",12332132112L,"17/2019");
        ConcursoResponseDTO concurso2 = new ConcursoResponseDTO("SEJUS",32132112312L,"15/2011");

        when(concursoService.buscarPorProfissoes(candidato.getProfissoes())).thenReturn(List.of(concurso1,concurso2));
        when(candidatoService.buscarPorCpf(cpf)).thenReturn(candidato);

        List<ConcursoResponseDTO> result = matchingService.buscarConcursosCompativeisPorCpf(cpf);
        assertThat(result).hasSize(2);
        assertThat(result).extracting(ConcursoResponseDTO::getCodigo).containsExactlyInAnyOrder(32132112312L, 12332132112L);

    }

    @Test
    @DisplayName("Deve lançar exceção do tipo NoMatchConcurso")
    void buscarConcursosCompativeisPorCpfCase2() throws Exception{
        String cpf = "12332132112";
        Candidato candidato = new Candidato(1L, "Joao", cpf,"13/04/2001",List.of("advogado","policial"));

        when(concursoService.buscarPorProfissoes(candidato.getProfissoes())).thenReturn(List.of());
        when(candidatoService.buscarPorCpf(cpf)).thenReturn(candidato);

        Exception thrown = Assertions.assertThrows(NoMatchConcursoException.class, ()->{
            List<ConcursoResponseDTO> result = matchingService.buscarConcursosCompativeisPorCpf(cpf);
        });
        Assertions.assertEquals("Nenhum Concurso compatível com este candidato",thrown.getMessage());

    }

    @Test
    @DisplayName("Deve retornar com sucesso candidatos compativeis")
    void buscarCandidatosCompativeisPorCodigoConcursoCase1() {
        Long codigo = 12332112312L;

        CandidatoResponseDTO candidato1 = new CandidatoResponseDTO("Joao","13/04/2001","12332132112");
        CandidatoResponseDTO candidato2 = new CandidatoResponseDTO("cleber","11/06/2003","12352132112");

        Concurso concurso1 = new Concurso(1L,"SEJUS","14/2024",codigo,List.of("carpinteiro"));
        Concurso concurso2 = new Concurso(2L,"SEJUS","16/2023",codigo,List.of("jornalista","professor de matematica"));

        when(concursoService.buscarPorCodigo(codigo)).thenReturn(List.of(concurso1,concurso2));
        when(candidatoService.buscarPorProfissoes(Set.of("jornalista","carpinteiro","professor de matematica"))).thenReturn(List.of(candidato1, candidato2));

        List<CandidatoResponseDTO> result = matchingService.buscarCandidatosCompativeisPorCodigoConcurso(codigo);
        assertThat(result).hasSize(2);
        assertThat(result).extracting(CandidatoResponseDTO::getCpf).containsExactlyInAnyOrder("12332132112","12352132112");

    }

    @Test
    @DisplayName("Deve lançar exceção do tipo NoMatchCandidato")
    void buscarCandidatosCompativeisPorCodigoConcursoCase2() throws Exception {
        Long codigo = 12332112312L;

        Concurso concurso1 = new Concurso(1L,"SEJUS","14/2024",codigo,List.of("carpinteiro"));
        Concurso concurso2 = new Concurso(2L,"SEJUS","16/2023",codigo,List.of("jornalista","professor de matematica"));

        when(concursoService.buscarPorCodigo(codigo)).thenReturn(List.of(concurso1,concurso2));
        when(candidatoService.buscarPorProfissoes(Set.of("jornalista","carpinteiro","professor de matematica"))).thenReturn(List.of());

        Exception thrown = Assertions.assertThrows(NoMatchCandidatoException.class, ()->{
            List<CandidatoResponseDTO> result = matchingService.buscarCandidatosCompativeisPorCodigoConcurso(codigo);
        });

        Assertions.assertEquals("Nenhum Candidato compativel com este concurso",thrown.getMessage());
    }
}