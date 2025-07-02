package Pedro.Artur.BackendDesafioLeds.repository;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface ConcursoRepository extends JpaRepository<Concurso, Long> {
    @Query("Select c From Concurso c JOIN c.profissoes p WHERE p IN :profissoes")
    List<Concurso> BuscarConcursosCompativeisPorCpf(@Param("profissoes") List<String> profissoes);

    List<Concurso> findByCodigo(Long codigo);
}
