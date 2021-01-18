import { RegisterService } from '../../../services/entry/register.service';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ListDataSource } from './list-datasource';
import { listModel } from '../../../models/entry/listModel';

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

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ["cc","idVehicle","entryTime","idVehicleType"];

  constructor(private registerService: RegisterService) {}

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
