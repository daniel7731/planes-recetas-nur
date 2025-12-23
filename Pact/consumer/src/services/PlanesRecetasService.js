import axios from "axios";

export class PlanesRecetasService {
    baseUrl = 'https://localhost:44320';

    constructor(theUrl) {
        if (theUrl) {
            this.baseUrl = theUrl;
        }
    }
    getUnidades() {
        return new Promise((resolve, reject) => {
            axios.get(this.baseUrl + '/api/Unidad/GetAll')
                .then(response => {
                    resolve(response.data);
                })
                .catch(error => {
                    reject(error);
                });
        });
    }
    
    createPaciente(paciente) {
        return new Promise((resolve, reject) => {
            axios.post(this.baseUrl + '/api/Paciente/CreatePaciente', paciente)
                .then(response => {
                    resolve(response.data);
                })
                .catch(error => {
                    reject(error);
                });
        });
    }
}