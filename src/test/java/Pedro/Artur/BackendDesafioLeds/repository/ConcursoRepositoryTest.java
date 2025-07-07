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
    @DisplayName("Deve devolver lista de concursos compativeis")
    void buscarConcursosCompativeisPorProfissoesCase1() {

    }

    @Test
    @DisplayName("Deve devolver lista de concursos com codigo x")
    void findByCodigoCase1() {
        Long codigo_valido = 95655123539L;
        this.createConcurso("SEJUS","17/2024",codigo_valido,List.of("medico","advogado"));
        List<Concurso> result = concursoRepository.findByCodigo(codigo_valido);
        assertThat(result.isEmpty()).isFalse();
    }

    @Test
    @DisplayName("Não deve devolver lista de concursos compativeis")
    void buscarConcursosCompativeisPorProfissoesCase2() {
    }

    @Test
    @DisplayName("Não deve devolver lista de concursos")
    void findByCodigoCase2() {
        Long codigo_invalido = 12345678912L;
        List<Concurso> result = concursoRepository.findByCodigo(codigo_invalido);
        assertThat(result.isEmpty()).isTrue();
    }

    private Concurso createConcurso(String orgao, String edital, Long codigo, List<String> profissoes){
        Concurso concurso = new Concurso(null,orgao,edital,codigo,profissoes);
        this.entityManager.persist(concurso);
        return concurso;
    }
}