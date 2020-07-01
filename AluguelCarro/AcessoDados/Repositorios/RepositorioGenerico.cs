using AluguelCarro.Models;
using FichaAcademia.AcessoDados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FichaAcademia.AcessoDados.Repositorios
{
    public abstract class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
    {
        protected readonly Contexto _contexto;

        public RepositorioGenerico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task Atualizar(TEntity entity)
        {
            try
            {
                _contexto.Set<TEntity>().Update(entity);
                await _contexto.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(int id)
        {
            try
            {
                var entity = await PegarPeloId(id);
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Excluir(string id)
        {
            try
            {
                var entity = await PegarPeloId(id);
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Inserir(TEntity entity)
        {
            try
            {
                await _contexto.Set<TEntity>().AddAsync(entity);
                await _contexto.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(int? id)
        {
            try
            {
                return await _contexto.Set<TEntity>().FindAsync(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(string id)
        {
            try
            {
                return await _contexto.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<TEntity> PegarTodos()
        {
            return _contexto.Set<TEntity>();
        }
    }
}
