CREATE OR REPLACE PROCEDURE mover_para_backup(p_id INT)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO contato_backup (id, nome, apelido, cpf, telefone, email, data_cadastro, data_ultima_alteracao, data_exclusao)
    SELECT id, nome, apelido, cpf, telefone, email, data_cadastro, data_ultima_alteracao, NOW()
    FROM contato
    WHERE id = p_id;

    DELETE FROM contato WHERE id = p_id;
END;
$$;