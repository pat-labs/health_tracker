"""mermaid
graph LR
subgraph Client
A[Client Applications] --> B(gRPC Client);
end

    B --> C{API Gateway};

    subgraph .NET Core API
        C --> D[ASP.NET Core API];
        D --> E{Business Logic};
        E --> F[Data Access Layer];
    end

    subgraph Data Storage
        F --> G[TimescaleDB PostgreSQL];
        E --> H[Redis Event Store];
    end

    style G fill:#ccf,stroke:#888,stroke-width:2px
    style H fill:#fcc,stroke:#888,stroke-width:2px
    style D fill:#cff,stroke:#888,stroke-width:2px
    style B fill:#cfc,stroke:#888,stroke-width:2px
    style C fill:#fcf,stroke:#888,stroke-width:2px

    classDef database fill:#ccf,stroke:#888,stroke-width:2px
    class G,H database

    classDef api fill:#cff,stroke:#888,stroke-width:2px
    class D api

    classDef client fill:#cfc,stroke:#888,stroke-width:2px
    class B client

    classDef gateway fill:#fcf,stroke:#888,stroke-width:2px
    class C gateway

    linkStyle 0 stroke:#0a0,stroke-width:2px;
    linkStyle 1 stroke:#0a0,stroke-width:2px;
    linkStyle 2 stroke:#00a,stroke-width:2px;
    linkStyle 3 stroke:#00a,stroke-width:2px;
    linkStyle 4 stroke:#a00,stroke-width:2px;
    linkStyle 5 stroke:#a00,stroke-width:2px;

    direction LR
    subgraph Communication Protocol
        I[Protobuf gRPC] -.-> B
        I -.-> C
    end

"""
