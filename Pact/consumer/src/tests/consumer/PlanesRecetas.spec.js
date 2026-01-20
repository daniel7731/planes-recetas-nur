import { MatchersV3, PactV3 } from "@pact-foundation/pact";
import { describe, it } from "mocha";
import { PlanesRecetasService } from "../../services/PlanesRecetasService.js";
import { expect } from "chai";
import { responseUnidadesList } from "../PactResponsesUnidades.js";
import { responseCategoryList } from "../PactResponseCategoria.js";
import { responseNutricionistaList } from "../PactResponseNutricionistas.js";
import { reponsesTiempo} from "../PactResponsesTiempo.js";
import { postResponseSuccess} from "../PactResponsePost.js";
import { generatePacienteDataFaker } from "../PactResponsePaciente.js";
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

    describe('obtener lista de categorias', () => {
        it('retorna una lista de categorias', () => {
            //Arrange
            provider.given('realizar una consulta de categorias')
                .uponReceiving('Un body vacío')
                .withRequest({
                    method: 'GET',
                    path: '/api/Categoria/GetAll'
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
                return planesRecetasService.getCategoriaAll().then((response) => {
                    // Assert

                    expect(response).to.be.not.null;
                    expect(response).to.be.a.string;
                    expect(response).to.deep.equal(responseCategoryList);
                    expect(response).to.be.an('array');
                    expect(response).to.not.be.empty;
                    const names = response.map(item => item.nombre);
                    expect(names).to.include('Verdura de raíz');
                    expect(names).to.include('Arroz');

                });
            });
        });
    });

    describe('obtener lista de nutricionistas', () => {
        it('retorna una lista de nutricionistas', () => {
            //Arrange
            provider.given('realizar una consulta de nutricionistas')
                .uponReceiving('Un body vacío')
                .withRequest({
                    method: 'GET',
                    path: '/api/Nutricionistas/GetAll'
                }).willRespondWith({
                    status: 200,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: like(responseNutricionistaList)
                });
            return provider.executeTest(async mockServer => {
                // Act
                planesRecetasService = new PlanesRecetasService(mockServer.url);
                return planesRecetasService.getNutricionistaAll().then((response) => {
                    // Assert

                    expect(response).to.be.not.null;
                    expect(response).to.be.a.string;
                    expect(response).to.deep.equal(responseNutricionistaList);
                    expect(response).to.be.an('array');
                    expect(response).to.not.be.empty;
                   

                });
            });
        });
    });

    describe('obtener lista de tiempo de comidas', () => {
        it('retorna una lista de tiempo', () => {
            //Arrange
            provider.given('realizar una consulta de tiempo')
                .uponReceiving('Un body vacío')
                .withRequest({
                    method: 'GET',
                    path: '/api/Tiempo/GetAll'
                }).willRespondWith({
                    status: 200,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: like(reponsesTiempo)
                });
            return provider.executeTest(async mockServer => {
                // Act
                planesRecetasService = new PlanesRecetasService(mockServer.url);
                return planesRecetasService.getTiempoAll().then((response) => {
                    // Assert

                    expect(response).to.be.not.null;
                    expect(response).to.be.a.string;
                    expect(response).to.deep.equal(reponsesTiempo);
                    expect(response).to.be.an('array');
                    expect(response).to.have.lengthOf(5);
                    const values = response.map((tiempo) => tiempo.nombre);
                    expect(values).to.include('Breaskfast');

                });
            });
        });
    });

    describe('Crear un paciente', () => {
        it('retorna objeto response con la id, generado y a boolean field isSuccess', () => {
            //Arrange
            const paciente = generatePacienteDataFaker();
            console.log(paciente);
            provider.given('crear paciente')
                .uponReceiving('datos para crear un paciente')
                .withRequest({
                    method: 'POST',
                    path: '/api/Paciente/CreatePaciente',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: paciente
                }).willRespondWith({
                    status: 200,
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: like(postResponseSuccess)
                });
            return provider.executeTest(async mockServer => {
                // Act
                planesRecetasService = new PlanesRecetasService(mockServer.url);
                return planesRecetasService.createPaciente(paciente).then((response) => {
                    //Assert
                    expect(response).to.be.not.null;
                    expect(response).to.be.a.string;
                    expect(response).to.deep.equal(postResponseSuccess);
                    expect(response.isSuccess).to.equal(true);
                });
            });

        })
    });
});


