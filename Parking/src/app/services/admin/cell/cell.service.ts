import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CellService {

  constructor(private http: HttpClient) {}

  getAllCells(){
    return this.http.get<any>(`${environment.url_api}/Cells`);
  }
}
