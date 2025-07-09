import { validateCpf, validateCodigoConcurso, validateDate } from '../../utils/validators';
import { ValidationError } from '../../domain/errors';

describe('Validators', () => {
  describe('validateCpf', () => {
    it('should accept valid CPF format', () => {
      expect(() => validateCpf('123.456.789-01')).not.toThrow();
      expect(() => validateCpf('000.000.000-00')).not.toThrow();
    });

    it('should reject invalid CPF format', () => {
      expect(() => validateCpf('12345678901')).toThrow(ValidationError);
      expect(() => validateCpf('123.456.789-1')).toThrow(ValidationError);
      expect(() => validateCpf('123.456.789-012')).toThrow(ValidationError);
      expect(() => validateCpf('abc.def.ghi-jk')).toThrow(ValidationError);
      expect(() => validateCpf('')).toThrow(ValidationError);
    });
  });

  describe('validateCodigoConcurso', () => {
    it('should accept valid codigo format', () => {
      expect(() => validateCodigoConcurso('12345678901')).not.toThrow();
      expect(() => validateCodigoConcurso('00000000000')).not.toThrow();
    });

    it('should reject invalid codigo format', () => {
      expect(() => validateCodigoConcurso('123456789')).toThrow(ValidationError);
      expect(() => validateCodigoConcurso('123456789012')).toThrow(ValidationError);
      expect(() => validateCodigoConcurso('1234567890a')).toThrow(ValidationError);
      expect(() => validateCodigoConcurso('')).toThrow(ValidationError);
    });
  });

  describe('validateDate', () => {
    it('should accept valid date format', () => {
      const result = validateDate('25/12/2000');
      expect(result).toEqual(new Date(2000, 11, 25));
    });

    it('should reject invalid date format', () => {
      expect(() => validateDate('2000-12-25')).toThrow(ValidationError);
      expect(() => validateDate('25/12')).toThrow(ValidationError);
      expect(() => validateDate('25/13/2000')).toThrow(ValidationError);
      expect(() => validateDate('32/12/2000')).toThrow(ValidationError);
      expect(() => validateDate('')).toThrow(ValidationError);
    });
  });
});
