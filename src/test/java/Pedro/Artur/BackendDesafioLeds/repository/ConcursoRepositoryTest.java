package Pedro.Artur.BackendDesafioLeds.repository;

import Pedro.Artur.BackendDesafioLeds.model.Concurso;

import jakarta.persistence.EntityManager;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;

import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;

@DataJpaTest
class ConcursoRepositoryTest {

    @Autowired
    ConcursoRepository concursoRepository;

    @Autowired
    EntityManager entityManager;

    @Test
    @DisplayName("Deve devolver lista de concursos")
    void buscarConcursosCompativeisPorProfissoesCase1() {
        List<String> profissoes = List.of("medico","advogado");
        this.createConcurso("SEJUS","17/2024",95655123539L,profissoes);
        List<Concurso> result = concursoRepository.buscarConcursosCompativeisPorProfissoes(profissoes);
        assertThat(result).isNotEmpty();
        assertThat(result).hasSize(1);
        assertThat(result).extracting(Concurso::getEdital).containsExactly("17/2024");
    }

    @Test
    @DisplayName("Não deve devolver lista de concursos compativeis quando nao existir")
    void buscarConcursosCompativeisPorProfissoesCase2() {
        Long codigo_invalido = 95655123539L;
        List<Concurso> result = concursoRepository.findByCodigo(codigo_invalido);
        assertThat(result).isEmpty();
    }

    @Test
    @DisplayName("Deve devolver lista de concursos com codigo x")
    void findByCodigoCase1() {
        Long codigo_valido = 95655123539L;
        this.createConcurso("SEJUS","17/2024",codigo_valido,List.of("medico","advogado"));
        this.createConcurso("SEJUS","12/2021",codigo_valido,List.of("enfermeiro","marceneiro"));
        List<Concurso> result = concursoRepository.findByCodigo(codigo_valido);
        assertThat(result).isNotEmpty();
        assertThat(result).hasSize(2);
        assertThat(result).extracting(Concurso::getEdital).containsExactlyInAnyOrder("12/2021","17/2024");
    }

    @Test
    @DisplayName("Não deve devolver lista de concursos quando codigo nao existe")
    void findByCodigoCase2() {
        Long codigo_invalido = 12345678912L;
        List<Concurso> result = concursoRepository.findByCodigo(codigo_invalido);
        assertThat(result).isEmpty();
    }

    private Concurso createConcurso(String orgao, String edital, Long codigo, List<String> profissoes){
        Concurso concurso = new Concurso(null,orgao,edital,codigo,profissoes);
        this.entityManager.persist(concurso);
        return concurso;
    }
}