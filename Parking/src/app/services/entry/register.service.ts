import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { registerFormModel } from '../../models/entry/registerFormModel';
import { environment } from '../../../environments/environment';
import { listModel } from '../../models/entry/listModel';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }

  createEntry(register: registerFormModel) {
    return this.http.post(`${environment.url_api}/Entry`, register);
  }

  getListEntries() {
    return this.http.get<listModel[]>(`${environment.url_api}/Entry`);
  }

  getWeatherTest() {
    return this.http.get<any[]>(`${environment.url_api}/weatherforecast`);
  }
}
