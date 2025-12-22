import { MatchersV3, PactV3 } from "@pact-foundation/pact";
import { describe, it } from "mocha";
import { PlanesRecetasService } from "../../services/PlanesRecetasService.js";
import { expect } from "chai";
import { crearItemRequestBody, crearItemResponse, responseUnidadesList } from "../PactResponsesUnidades.js";
const { like } = MatchersV3;

describe('El API de Planes recetas', () => {

    let planesRecetasService;
    const provider = new PactV3({
        consumer: 'consumer-planes-y-recetas',
        provider: 'planes-recetas'
    });

    describe('obtener lista de unidades', () => {
        it('retorna una lista de unidades', () => {
            //Arrange
            provider.given('realizar una consulta de unidades')
                .uponReceiving('Un body vacío')
                .withRequest({
                    method: 'GET',
                    path: '/api/Unidad/GetAll'
                }).willRespondWith({
                    status: 200,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: like(responseUnidadesList)
                });
            return provider.executeTest(async mockServer => {
                // Act
                planesRecetasService = new PlanesRecetasService(mockServer.url);
                return planesRecetasService.getUnidades().then((response) => {
                    // Assert

                    expect(response).to.be.not.null;
                    expect(response).to.be.a.string;
                    expect(response).to.deep.equal(responseUnidadesList);
                    expect(response).to.be.an('array');
                    expect(response).to.have.lengthOf(4);
                    const values = response.map((unidad) => unidad.nombre);
                    expect(values).to.include('Kilogramos');

                });
            });
        });
    });
    /*describe('Agregar un item', () => {
        it('retorna un id de item ya creado', () => {
            //Arrange
            provider.given('crear item')
                .uponReceiving('datos para crear un item')
                .withRequest({
                    method: 'POST',
                    path: '/api/Item',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: crearItemRequestBody
                }).willRespondWith({
                    status: 200,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: like(crearItemResponse)
                });
            return provider.executeTest(async mockServer => {
                // Act
                PlanesRecetasService = new ItemService(mockServer.url);
                return PlanesRecetasService.createItem(crearItemRequestBody).then((response) => {
                    //Assert
                    expect(response).to.be.not.null;
                    expect(response).to.be.a.string;
                    expect(response).to.deep.equal(crearItemResponse);
                    expect(response.value).to.equal(crearItemRequestBody.id);
                });
            });

        })
    });*/
});


