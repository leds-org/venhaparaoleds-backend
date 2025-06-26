const pgErrorMap = {
    // Erros de Conexão e Rede
    '08000': { // connection_exception
        message: 'Falha na conexão com o banco de dados.',
        httpStatus: 500,
        type: 'ConnectionError'
    },
    '08006': { // connection_failure
        message: 'Conexão com o banco de dados perdida.',
        httpStatus: 500,
        type: 'ConnectionError'
    },
    '08003': { // connection_does_not_exist
        message: 'A conexão com o banco de dados não existe ou foi fechada.',
        httpStatus: 500,
        type: 'ConnectionError'
    },
    '08001': { // sqlclient_unable_to_establish_sqlconnection
        message: 'Não foi possível estabelecer uma conexão com o banco de dados.',
        httpStatus: 500,
        type: 'ConnectionError'
    },

    // Erros de Dados e Integridade (Violations)
    '22001': { // string_data_right_truncation
        message: 'Dados de string muito longos para a coluna.',
        httpStatus: 400,
        type: 'DataError'
    },
    '22003': { // numeric_value_out_of_range
        message: 'Valor numérico fora do intervalo permitido.',
        httpStatus: 400,
        type: 'DataError'
    },
    '22007': { // invalid_datetime_format
        message: 'Formato de data/hora inválido.',
        httpStatus: 400,
        type: 'DataError'
    },
    '22P02': { // invalid_text_representation
        message: 'Representação de texto inválida para o tipo de dado.',
        httpStatus: 400,
        type: 'DataError'
    },
    '23502': { // not_null_violation
        message: 'Campo obrigatório não pode ser nulo.',
        httpStatus: 400,
        type: 'IntegrityConstraintViolation'
    },
    '23503': { // foreign_key_violation
        message: 'Violação de chave estrangeira. Recurso relacionado não existe ou não pode ser excluído.',
        httpStatus: 400, // Ou 409 se for uma tentativa de exclusão de pai com filhos
        type: 'IntegrityConstraintViolation'
    },
    '23505': { // unique_violation
        message: 'Valor duplicado. Este registro já existe.',
        httpStatus: 409, // Conflict
        type: 'IntegrityConstraintViolation'
    },
    '23514': { // check_violation
        message: 'Violação de restrição CHECK. O valor fornecido não atende aos requisitos.',
        httpStatus: 400,
        type: 'IntegrityConstraintViolation'
    },
    '23000': { // integrity_constraint_violation
        message: 'Violação geral de integridade de dados.',
        httpStatus: 400,
        type: 'IntegrityConstraintViolation'
    },

    // Erros de Sintaxe e Schema
    '42P01': { // undefined_table
        message: 'Tabela ou relação não existe.',
        httpStatus: 500,
        type: 'SchemaError'
    },
    '42703': { // undefined_column
        message: 'Coluna não existe na tabela.',
        httpStatus: 500,
        type: 'SchemaError'
    },
    '42601': { // syntax_error
        message: 'Erro de sintaxe na consulta SQL.',
        httpStatus: 500,
        type: 'SchemaError'
    },
    '42883': { // undefined_function
        message: 'Função SQL não existe.',
        httpStatus: 500,
        type: 'SchemaError'
    },
    '42P07': { // duplicate_table
        message: 'A tabela que você está tentando criar já existe.',
        httpStatus: 500,
        type: 'SchemaError'
    },
    '42P08': { // duplicate_function
        message: 'A função que você está tentando criar já existe.',
        httpStatus: 500,
        type: 'SchemaError'
    },
    '42P09': { // duplicate_column
        message: 'A coluna que você está tentando adicionar já existe.',
        httpStatus: 500,
        type: 'SchemaError'
    },

    // Erros de Transação
    '25000': { // invalid_transaction_state
        message: 'Estado de transação inválido.',
        httpStatus: 500,
        type: 'TransactionError'
    },
    '25P02': { // no_active_transaction
        message: 'Nenhuma transação ativa para a operação.',
        httpStatus: 500,
        type: 'TransactionError'
    },
    '40001': { // serialization_failure
        message: 'Falha de serialização devido a concorrência. Tente novamente a operação.',
        httpStatus: 409, // Conflict, pode ser retentável pelo cliente
        type: 'ConcurrencyError'
    },
    '40P01': { // deadlock_detected
        message: 'Deadlock detectado. Transação abortada. Tente novamente.',
        httpStatus: 409, // Conflict, pode ser retentável pelo cliente
        type: 'ConcurrencyError'
    },

    // Erros de Permissão
    '42501': { // insufficient_privilege
        message: 'Permissões insuficientes para a operação.',
        httpStatus: 403, // Forbidden
        type: 'PermissionError'
    },

    // Erros Internos / Genéricos
    'XX000': { // internal_error
        message: 'Erro interno inesperado do banco de dados.',
        httpStatus: 500,
        type: 'InternalError'
    },
    '53000': { // insufficient_resources
        message: 'Recursos insuficientes no banco de dados para completar a operação.',
        httpStatus: 500,
        type: 'ResourceError'
    },
    '53100': { // disk_full
        message: 'Disco cheio no servidor de banco de dados.',
        httpStatus: 500,
        type: 'ResourceError'
    },
    '53200': { // out_of_memory
        message: 'Memória insuficiente no servidor de banco de dados.',
        httpStatus: 500,
        type: 'ResourceError'
    },

    // Outros erros comuns
    '22012': { // division_by_zero
        message: 'Tentativa de divisão por zero.',
        httpStatus: 400,
        type: 'DataError'
    },
    '0A000': { // feature_not_supported
        message: 'Funcionalidade SQL não suportada pelo banco de dados.',
        httpStatus: 500,
        type: 'FeatureError'
    }
};

module.exports = pgErrorMap;