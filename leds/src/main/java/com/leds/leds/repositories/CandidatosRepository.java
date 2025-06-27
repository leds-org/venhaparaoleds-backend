package com.leds.leds.repositories;

import java.util.List;
import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.leds.leds.models.Candidatos;

public interface CandidatosRepository extends JpaRepository<Candidatos, Long> {

    @Query("SELECT DISTINCT c FROM Candidatos c JOIN c.profissoes p WHERE LOWER(TRIM(p.nome)) IN :profissoes")
    List<Candidatos> findByProfissoesIn(@Param("profissoes") List<String> profissoes);

    Optional<Candidatos> findByCpf(String cpf);
}
