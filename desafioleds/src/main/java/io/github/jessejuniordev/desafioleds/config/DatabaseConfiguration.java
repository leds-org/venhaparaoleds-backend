package io.github.jessejuniordev.desafioleds.config;

import com.zaxxer.hikari.HikariConfig;
import com.zaxxer.hikari.HikariDataSource;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import javax.sql.DataSource;

@Configuration
public class DatabaseConfiguration {


    @Value("${spring.datasource.url}")
    private String url;

    @Value("${spring.datasource.username}")
    private String username;

    @Value("${spring.datasource.password}")
    private String password;

    @Value("${spring.datasource.driver-class-name}")
    private String driver;

    /**
     * Configuração Hikari
     * https://github.com/brettwooldridge/HikariCP
     * @return DataSource pool
     */
    @Bean
    public DataSource hikariDataSource() {
        HikariConfig config = new HikariConfig();

        config.setJdbcUrl(url);
        config.setUsername(username);
        config.setPassword(password);
        config.setDriverClassName(driver);

        config.setMaximumPoolSize(10); // Máximo de conexões liberadas
        config.setMinimumIdle(1); // Tamanho inicial do pool
        config.setPoolName("desafioleds-db-pool");
        config.setMaxLifetime(600000); // 600 mil ms -> 10 minutos
        config.setConnectionTimeout(100000); // Timeout para conseguir uma conexão
        config.setConnectionTestQuery("select 1"); // query teste

        return new HikariDataSource(config);
    }

}
