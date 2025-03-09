package io.github.jessejuniordev.desafioleds.repository;

import io.github.jessejuniordev.desafioleds.model.Candidato;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;
import java.util.UUID;


public interface CandidatoRepository extends JpaRepository<Candidato, UUID> {
    Optional<Candidato> findByCpf(String cpf);
}
