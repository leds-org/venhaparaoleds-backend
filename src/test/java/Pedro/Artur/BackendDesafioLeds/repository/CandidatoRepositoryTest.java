package Pedro.Artur.BackendDesafioLeds.repository;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import jakarta.persistence.EntityManager;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.test.context.ActiveProfiles;

import java.util.Arrays;
import java.util.List;
import java.util.Set;

import static org.assertj.core.api.Assertions.assertThat;

@DataJpaTest
@ActiveProfiles("test")
class CandidatoRepositoryTest {

    @Autowired
    CandidatoRepository candidatoRepository;

    @Autowired
    EntityManager entityManager;

    @Test
    @DisplayName("Deve encontrar um candidato")
    void findByCpfCase1() {
        String cpf_valido = "11122233345";
        this.createCandidato(
                "Artur",
                cpf_valido,
                "12/05/2000"
                ,Arrays.asList("carpinteiro","lenhador"));

        Candidato result = this.candidatoRepository.findByCpf(cpf_valido);
        assertThat(result).isNotNull();
        assertThat(result.getCpf()).isEqualTo(cpf_valido);
    }

    @Test
    @DisplayName("Não deve encontrar um candidato")
    void findByCpfCase2() {
        String cpf_inexistente = "11122233346";
        Candidato result = this.candidatoRepository.findByCpf(cpf_inexistente);
        assertThat(result).isNull();
    }

    @Test
    @DisplayName("Deve retornar lista de candidatos compativeis por profissao")
    void buscarCandidatosCompativeisCase1(){
        Set<String> profissoesConcurso = Set.of("carpinteiro", "medico");

        this.createCandidato("Pedro","22233344455","06/08/2001",List.of("carpinteiro"));
        this.createCandidato("Jorge","33344455567","12/07/1990",List.of("medico","lenhador"));

        List<Candidato> candidatosResult = this.candidatoRepository.buscarCandidatosCompativeis(profissoesConcurso);
        assertThat(candidatosResult).isNotEmpty();
        assertThat(candidatosResult).hasSize(2);
        assertThat(candidatosResult).extracting(Candidato::getNome).containsExactlyInAnyOrder("Pedro","Jorge");

    }
    @Test
    @DisplayName("Não Deve retornar lista de candidatos")
    void buscarCandidatosCompativeisCase2(){
        Set<String> profissoesConcurso = Set.of("jardineiro","jornalista");

        this.createCandidato("Pedro","22233344455","06/08/2001",List.of("carpinteiro"));
        this.createCandidato("Jorge","33344455567","12/07/1990",List.of("medico","lenhador"));

        List<Candidato> candidatosResult = this.candidatoRepository.buscarCandidatosCompativeis(profissoesConcurso);
        assertThat(candidatosResult).isEmpty();

    }

    private Candidato createCandidato(String nome, String cpf, String dataNasc, List<String> profissoes){
        Candidato candidato = new Candidato(null, nome,cpf,dataNasc,profissoes);
        this.entityManager.persist(candidato);
        return candidato;
    }
}