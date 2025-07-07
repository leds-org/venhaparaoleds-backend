package Pedro.Artur.BackendDesafioLeds.repository;

import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;
import java.util.Set;

public interface CandidatoRepository extends JpaRepository<Candidato, Long> {
    @Query("Select c From Candidato c JOIN c.profissoes p WHERE p IN :profissoes")
    List<Candidato> buscarCandidatosCompativeis(@Param("profissoes") Set<String> profissoes);

    Candidato findByCpf(String cpf);
}
