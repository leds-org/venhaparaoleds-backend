package io.github.jessejuniordev.desafioleds.repository;

import io.github.jessejuniordev.desafioleds.model.Concurso;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.UUID;

public interface ConcursoRepository extends JpaRepository<Concurso, UUID> {
    List<Concurso> findByCodigoConcurso(String codigoConcurso);
}
