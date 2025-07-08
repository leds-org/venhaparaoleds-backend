package Pedro.Artur.BackendDesafioLeds.utils;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

class CodigoUtilsTest {

    @Test
    @DisplayName("Deve retornar true quando codigo valido")
    void validarCodigoCase1() {
        Long codigo_valido = 12332132112L;
        boolean result = CodigoUtils.validarCodigo(codigo_valido);
        assertThat(result).isTrue();
    }

    @Test
    @DisplayName("Deve retornar false quando codigo negativo")
    void validarCodigoCase2() {
        Long codigo_invalido = -123L;
        boolean result = CodigoUtils.validarCodigo(codigo_invalido);
        assertThat(result).isFalse();
    }

    @Test
    @DisplayName("Deve retornar false quando codigo é nulo")
    void validarCodigoCase3() {
        Long codigo_invalido = null;
        boolean result = CodigoUtils.validarCodigo(codigo_invalido);
        assertThat(result).isFalse();
    }

}