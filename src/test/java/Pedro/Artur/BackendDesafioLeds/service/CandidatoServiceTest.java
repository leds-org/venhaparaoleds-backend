package Pedro.Artur.BackendDesafioLeds.service;


import Pedro.Artur.BackendDesafioLeds.exception.InvalidParameterException;
import Pedro.Artur.BackendDesafioLeds.exception.NotFoundCpfException;
import Pedro.Artur.BackendDesafioLeds.model.Candidato;
import Pedro.Artur.BackendDesafioLeds.repository.CandidatoRepository;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.beans.factory.annotation.Autowired;
import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.when;

class CandidatoServiceTest {
    @Mock
    CandidatoRepository candidatoRepository;

    @Autowired
    @InjectMocks
    CandidatoService candidatoService;

    @BeforeEach
    void setup(){
        MockitoAnnotations.initMocks(this);
    }

    @Test
    @DisplayName("Deve retornar com sucesso um candidato")
    void buscarPorCpfCase1() {
        String cpf_valido_formatado = "123.321.333-45";
        String cpf_valido_limpo = "12332133345";
        Candidato candidato = new Candidato(1L, "Pedro",cpf_valido_limpo,"13/05/1993",List.of("marceneiro"));
        when(candidatoRepository.findByCpf(cpf_valido_limpo)).thenReturn(candidato);

        Candidato result = candidatoService.buscarPorCpf(cpf_valido_formatado);

        assertThat(result).isNotNull();
        assertThat(result.getCpf()).isEqualTo(cpf_valido_limpo);
        assertThat(result.getNome()).isEqualTo("Pedro");
    }

    @Test
    @DisplayName("Deve lançar um erro do tipo NotFoundCpf")
    void buscarPorCpfCase2() throws Exception {
        String cpf_valido = "332.333.334-29";
        when(candidatoRepository.findByCpf("33233333429")).thenReturn(null);

        Exception thrown = Assertions.assertThrows(NotFoundCpfException.class, () ->{
            Candidato result = candidatoService.buscarPorCpf(cpf_valido);
        });

        Assertions.assertEquals("Cpf de candidato não encontrado em nosso sistema!", thrown.getMessage());
    }

    @Test
    @DisplayName("Deve lançar um erro do tipo InvalidParameter")
    void buscarPorCpfCase3() throws Exception{
        String cpf_invalido = "332.333.334-292";

        Exception thrown = Assertions.assertThrows(InvalidParameterException.class, ()->{
            Candidato result = candidatoService.buscarPorCpf(cpf_invalido);
        });
        Assertions.assertEquals("Cpf Inválido", thrown.getMessage());

    }


}