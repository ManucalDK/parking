import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from '../../../environments/environment';
import { registerFormModel } from '../../models/departure/registerFormModel';
import { listModel } from '../../models/departure/listModel';

@Injectable({
  providedIn: 'root'
})
export class DepartureService {

  constructor(private http: HttpClient) { }

  createEntry(register: registerFormModel) {
    return this.http.post(`${environment.url_api}/Departure`, register);
  }

  getListEntries() {
    return this.http.get<listModel[]>(`${environment.url_api}/Departure`);
  }
}
