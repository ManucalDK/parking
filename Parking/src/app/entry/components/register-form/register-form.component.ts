import { RegisterService } from '../../../services/entry/register.service';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { registerFormModel } from '../../../models/entry/registerFormModel';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {
  
  registerForm = this.fb.group({
    placa: [null, Validators.required],
    tipoVehiculo: [null, Validators.required],
    cilindraje: [null]
  });

  hasUnitNumber = false;
  isMotorcycle = true;
  generalError:string = "";

  states = [
    {name: 'Carro', abbreviation: 1},
    {name: 'Moto', abbreviation: 2},
  ];

  constructor(private fb: FormBuilder,
              private registerService: RegisterService) {}

  async onSubmit() {
    this.generalError = "";
    const product = await this.registerEntryAsync();
  }

  async registerEntryAsync(){
    const registerForm:registerFormModel = {
      IdVehicle:this.registerForm.get('placa')?.value,
      CC:this.registerForm.get('cilindraje')?.value,
      IdVehicleType: this.registerForm.get('tipoVehiculo')?.value,
    };
    
    return this.registerService.createEntry(registerForm).toPromise().catch((reason: HttpErrorResponse) => {
      this.generalError = reason.error;
    });
  }
}
