import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ListDataSource } from './list-datasource';
import { listModel } from '../../../models/departure/listModel';
import { DepartureService } from '../../../services/departure/departure.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<listModel>;
  dataSource!: ListDataSource;

  
  displayedColumns = ['idVehicle', 'rateValue', 'departureTime'];

  constructor(private registerService: DepartureService) {}

  ngOnInit() {
    this.getListEntries();
    this.dataSource = new ListDataSource();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  async getListEntries(){
    const list:listModel[] = await this.registerService.getListEntries().toPromise();
    this.table.dataSource = list;
  }
}
