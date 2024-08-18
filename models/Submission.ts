export class Submission {
    firstName: string;
    secondName: string;
    age: number | null;
    country: string;
    notes: string;
  
    constructor(firstName: string, secondName: string, age: number | null, country: string, notes: string) {
      this.firstName = firstName;
      this.secondName = secondName;
      this.age = age;
      this.country = country;
      this.notes = notes;
    }
  }
  