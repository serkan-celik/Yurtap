import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YoklamaBaslikListesiComponent } from './yoklama-baslik-listesi.component';

describe('YoklamaBaslikListesiComponent', () => {
  let component: YoklamaBaslikListesiComponent;
  let fixture: ComponentFixture<YoklamaBaslikListesiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YoklamaBaslikListesiComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YoklamaBaslikListesiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
