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
}