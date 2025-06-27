package com.leds.leds.repositories;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import com.leds.leds.models.Concursos;

public interface ConcursosRepository extends JpaRepository<Concursos, Long> {

    @Query("SELECT DISTINCT c FROM Concursos c JOIN c.listaDeVagas v WHERE LOWER(TRIM(v.cargo)) IN :profissoes")
    List<Concursos> buscarPorProfissoes(@Param("profissoes") List<String> profissoes);

    Concursos findByCodigoDoConcurso(String codigo);

}
