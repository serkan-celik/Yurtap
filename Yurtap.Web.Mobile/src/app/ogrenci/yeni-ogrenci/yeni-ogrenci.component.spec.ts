import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { YeniOgrenciComponent } from './yeni-ogrenci.component';

describe('YeniOgrenciComponent', () => {
  let component: YeniOgrenciComponent;
  let fixture: ComponentFixture<YeniOgrenciComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YeniOgrenciComponent ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YeniOgrenciComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
