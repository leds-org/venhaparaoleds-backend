package Pedro.Artur.BackendDesafioLeds.repository;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CandidatoRepository extends JpaRepository<Candidato, Long> {

}
