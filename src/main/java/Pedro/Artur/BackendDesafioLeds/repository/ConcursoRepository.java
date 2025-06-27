package Pedro.Artur.BackendDesafioLeds.repository;

import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ConcursoRepository extends JpaRepository<Concurso, Long> {
}
