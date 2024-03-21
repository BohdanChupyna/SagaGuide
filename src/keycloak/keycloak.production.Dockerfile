FROM quay.io/keycloak/keycloak:23.0 as builder

# Enable health and metrics support

ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true
ENV KC_DB=postgres
ENV KC_DB_SCHEMA=public
ENV KC_PROXY=edge
WORKDIR /opt/keycloak

FROM quay.io/keycloak/keycloak:23.0
COPY --from=builder /opt/keycloak/ /opt/keycloak/

ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true
ENV KC_DB=postgres
ENV KC_DB_SCHEMA=public
ENV KC_PROXY=edge
ENV KC_HOSTNAME=saga.guide

ENTRYPOINT ["/opt/keycloak/bin/kc.sh"]
CMD ["start"]