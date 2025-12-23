// If using @faker-js/faker
import { faker } from '@faker-js/faker';

export const generatePacienteDataFaker = () => ({
  guid: faker.string.uuid(),
  nombre: faker.person.firstName(),
  apellido: faker.person.lastName(),
  fechaNacimiento: faker.date.birthdate().toISOString(),
  peso: faker.number.int({ min: 50, max: 150 }),
  altura: faker.number.int({ min: 140, max: 210 })
});
