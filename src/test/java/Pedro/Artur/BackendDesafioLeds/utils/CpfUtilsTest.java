package Pedro.Artur.BackendDesafioLeds.utils;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

class CpfUtilsTest {

    @Test
    @DisplayName("Deve retornar cpf limpo, formato->xxxxxxxxxxx")
    void limpar() {
        String cpf_sujo = "129 meu cas355 785 97";
        String resultado = CpfUtils.limpar(cpf_sujo);
        assertThat(resultado).isEqualTo("12935578597");
    }

    @Test
    @DisplayName("Deve retornar cpf formatado, formato->xxx.xxx.xxx-xx")
    void formatar() {
        String cpf_limpo = "12332145678";
        String resultado = CpfUtils.formatar(cpf_limpo);
        assertThat(resultado).isEqualTo("123.321.456-78");
    }

    @Test
    @DisplayName("Deve retornar true quando cpf tem apenas 11 numeros")
    void validarCaso1() {
        String cpf = "12332154666";
        Boolean resultado = CpfUtils.validar(cpf);
        assertThat(resultado).isTrue();
    }

    @Test
    @DisplayName("Deve retornar false quando cpf não tem apenas 11 numeros")
    void validarCaso2() {
        String cpf = "1233215466";
        Boolean resultado = CpfUtils.validar(cpf);
        assertThat(resultado).isFalse();
    }

}