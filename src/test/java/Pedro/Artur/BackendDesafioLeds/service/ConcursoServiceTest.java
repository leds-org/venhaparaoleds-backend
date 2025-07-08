package Pedro.Artur.BackendDesafioLeds.service;

import Pedro.Artur.BackendDesafioLeds.exception.InvalidParameterException;
import Pedro.Artur.BackendDesafioLeds.exception.NotFoundCodigoException;
import Pedro.Artur.BackendDesafioLeds.model.Concurso;
import Pedro.Artur.BackendDesafioLeds.repository.ConcursoRepository;
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

class ConcursoServiceTest {
    @Mock
    ConcursoRepository concursoRepository;

    @Autowired
    @InjectMocks
    ConcursoService concursoService;

    @BeforeEach
    void setup(){
        MockitoAnnotations.initMocks(this);
    }

    @Test
    @DisplayName("Deve retornar com sucesso um concurso")
    void buscarPorCodigoCase1() {
        Long codigo_valido = 12345678900L;
        Concurso concurso1 = new Concurso(1L, "SEJUS","17/2021",codigo_valido, List.of("vendedor","banqueiro"));
        Concurso concurso2 = new Concurso(2L, "SEJUS","12/2020",codigo_valido, List.of("padeiro","policial"));

        when(concursoRepository.findByCodigo(codigo_valido)).thenReturn(List.of(concurso1,concurso2));
        List<Concurso> result = concursoService.buscarPorCodigo(codigo_valido);
        assertThat(result).isNotEmpty();
        assertThat(result).extracting(Concurso::getEdital).containsExactlyInAnyOrder("17/2021","12/2020");
        assertThat(result).hasSize(2);

    }

    @Test
    @DisplayName("Deve lançar uma exceção do tipo NotFoundCodigo")
    void buscarPorCodigoCase2() throws Exception{
        Long codigo_invalido = 12345678911L;
        when(concursoRepository.findByCodigo(codigo_invalido)).thenReturn(List.of());

        Exception thrown = Assertions.assertThrows(NotFoundCodigoException.class,()->{
            List<Concurso> result = concursoService.buscarPorCodigo(codigo_invalido);
        });

        Assertions.assertEquals("Codigo de concurso não encontrado.", thrown.getMessage());

    }

    @Test
    @DisplayName("Deve lançar uma exceção do tipo InvalidParameter")
    void buscarPorCodigoCase3() throws Exception {
        Long codigo_invalido = null;

        Exception thrown = Assertions.assertThrows(InvalidParameterException.class,()->{
            List<Concurso> result = concursoService.buscarPorCodigo(codigo_invalido);
        });

        Assertions.assertEquals("Codigo invalido.", thrown.getMessage());

    }
}